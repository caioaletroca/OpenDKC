using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A standard character controller for 2D movement
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerController))]
public class RunController : MonoBehaviour
{
    #region Public Properties

    /// <summary>
    /// The force applied to the jump
    /// </summary>
    [SerializeField]
    private float JumpForce = 400f;

    /// <summary>
    /// Amount of speed when the character is crouching
    /// </summary>
    [SerializeField]
    [Range(0, 1)]
    private float CrouchSpeed = 0.36f;

    /// <summary>
    /// The speed for walking movement
    /// </summary>
    [SerializeField]
    private float WalkSpeed = 100;

    /// <summary>
    /// The speed for running movement
    /// </summary>
    [SerializeField]
    private float RunSpeed = 200;

    /// <summary>
    /// The smoothness for the movement
    /// </summary>
    [SerializeField]
    [Range(0, 0.3f)]
    private float MovementSmoothing = 0.5f;

    /// <summary>
    /// A flag that represents if the player can control his motion in mid-air
    /// </summary>
    [SerializeField]
    private bool AirControl = false;

    /// <summary>
    /// A Flag that represents if the player can controle his motion in crouch position
    /// </summary>
    [SerializeField]
    private bool CrouchControl = false;

    /// <summary>
    /// Determines the ground layer objects
    /// </summary>
    [SerializeField]
    private LayerMask GroundLayer;

    /// <summary>
    /// A empty game object that represents the ground calculation point
    /// </summary>
    [SerializeField]
    private Transform GroundPoint;

    /// <summary>
    /// A empty game object that represents the ceiling calculation point
    /// </summary>
    [SerializeField]
    private Transform CeilingPoint;

    /// <summary>
    /// A collider that will be disable when crouching
    /// </summary>
    [SerializeField]
    private Collider2D CrouchDisableCollider;

    #endregion

    #region Public Events

    [Header("Events")]
    [Space]

    /// <summary>
    /// Fires whenever to update the current ground distance
    /// </summary>
    public FloatEvent OnGroundDistanceEvent;

    /// <summary>
    /// Fires whenever the player touches the ground
    /// </summary>
    public UnityEvent OnLandEvent;

    /// <summary>
    /// Fires whenever the player crouches
    /// </summary>
    public BoolEvent OnCrouchEvent;

    /// <summary>
    /// Fires whenever the player turns its direcion
    /// </summary>
    public BoolEvent OnTurnEvent;

    [Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    [Serializable]
    public class FloatEvent : UnityEvent<float> { }

    #endregion

    #region Private Properties

    /// <summary>
    /// The object <see cref="Rigidbody2D"/>
    /// </summary>
    private Rigidbody2D mRigidbody2D;

    /// <summary>
    /// The radius for the overlap calculations on the grounded check
    /// </summary>
    const float mGroundedRadius = 0.2f;

    /// <summary>
    /// The radius for the overlap calculations on the ceiling check
    /// </summary>
    const float mCeilingRadius = 0.2f;

    /// <summary>
    /// A flag that represents if the player is on the ground
    /// </summary>
    private bool mGrounded;

    /// <summary>
    /// A flag that represents if the player is crouched
    /// </summary>
    private bool mCrouched;

    /// <summary>
    /// A flag that represents if the player is facing the right side
    /// </summary>
    private bool mFacingRight = true;

    /// <summary>
    /// A stored value for the current velocity
    /// </summary>
    private Vector2 TargetVelocity = Vector2.zero;

    /// <summary>
    /// Used for velocity calculation
    /// </summary>
    private Vector3 mVelocity = Vector3.zero;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        mRigidbody2D = GetComponent<Rigidbody2D>();

        if (OnGroundDistanceEvent == null)
            OnGroundDistanceEvent = new FloatEvent();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();

        if (OnTurnEvent == null)
            OnTurnEvent = new BoolEvent();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = mGrounded;
        mGrounded = false;

        // Check if the player touches the ground again
        Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundPoint.position, mGroundedRadius, GroundLayer);
        foreach (var collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                mGrounded = true;

                // Fire event when the player grounds
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Moves the character by walking or jumping
    /// </summary>
    /// <param name="move"></param>
    /// <param name="crouch"></param>
    /// <param name="jump"></param>
    public void Move(Vector2 move, bool Run, bool crouch, bool jump)
    {
        TargetVelocity = Vector2.zero;

        // Calculate the speed
        move.x *= (Run ? RunSpeed : WalkSpeed);

        // If player is crouching, check if the character could stand up
        if (!crouch)
            // If character has a ceiling preventing them from standing up, keep crouched
            if (Physics2D.OverlapCircle(CeilingPoint.position, mCeilingRadius, GroundLayer))
                crouch = true;

        // Let controllers do the work
        JumpController(move, crouch, jump);
        CrouchController(move, crouch, jump);

        // Apply smoothing to the movement
        mRigidbody2D.velocity = Vector3.SmoothDamp(mRigidbody2D.velocity, TargetVelocity, ref mVelocity, MovementSmoothing);

        // Calculate distance to the ground
        RaycastHit2D hit = Physics2D.Raycast(GroundPoint.transform.position, Vector2.down, Mathf.Infinity, GroundLayer);
        if (hit != null)
        {
            OnGroundDistanceEvent.Invoke(hit.distance);
        }
    }

    public void JumpEnemy()
    {
        mRigidbody2D.velocity = Vector2.zero;
        
        // Add vertical force to the player
        mGrounded = false;
        mRigidbody2D.AddForce(new Vector2(0f, JumpForce));
    }

    public void Attack()
    {
        
    }

    #endregion

    #region Private Methods

    private void JumpController(Vector2 move, bool crouch, bool jump)
    {
        // If the character should jump
        if (mGrounded && jump)
        {
            // Add vertical force to the player
            mGrounded = false;
            mRigidbody2D.AddForce(new Vector2(0f, JumpForce));
        }

        // If is in the air and has control
        if (AirControl && !mGrounded)
        {
            // Move the character by finding the target velocity
            TargetVelocity = new Vector2(move.x * 10f, mRigidbody2D.velocity.y);

            // Face the player to the right side depending on his movement
            if (move.x > 0 && !mFacingRight)
                Flip();
            else if (move.x < 0 && mFacingRight)
                Flip();
        }
    }

    private void CrouchController(Vector2 move, bool crouch, bool jump)
    {
        if (mGrounded)
        {
            // If the player is currently crouching
            if (crouch)
            {
                if (!mCrouched)
                {
                    mCrouched = true;
                    OnCrouchEvent.Invoke(true);
                }

                // Reduce the speed by the crouch speed multiplier
                move *= CrouchSpeed;

                // Disable the collider
                if (CrouchDisableCollider != null)
                    CrouchDisableCollider.enabled = false;
            }
            else
            {
                // Re-enables the collider when not crouching
                if (CrouchDisableCollider != null)
                    CrouchDisableCollider.enabled = true;

                if (mCrouched)
                {
                    mCrouched = false;
                    OnCrouchEvent.Invoke(false);
                }
            }

            if (!crouch || CrouchControl)
            {
                // Move the character by finding the target velocity
                TargetVelocity = new Vector2(move.x * 10f, mRigidbody2D.velocity.y);

                // Face the player to the right side depending on his movement
                if (move.x > 0 && !mFacingRight)
                    Flip();
                else if (move.x < 0 && mFacingRight)
                    Flip();
            }
        }
    }

    /// <summary>
    /// Flips the sprite for the character
    /// </summary>
    private void Flip()
    {
        // Switch the current player facing side
        mFacingRight = !mFacingRight;

        // Multiply the player's x local scale by -1
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        // Fire turn event
        OnTurnEvent.Invoke(true);
    }

    #endregion
}
