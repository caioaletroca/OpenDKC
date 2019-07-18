using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A standard character controller for 2D movement
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(KongController))]
public class RunController : BaseController
{
    #region Public Properties

    /// <summary>
    /// The force applied to the jump
    /// </summary>
    [SerializeField]
    [TooltipAttribute("The force applied to the jump")]
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
    [Space(10)]

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
    /// Instance of <see cref="Animator"/>
    /// </summary>
    private Animator animator;

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
    private bool Airborning = false;

    [HideInInspector]
    public bool Attacking = false;

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
        animator = GetComponent<Animator>();

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
        var wasAirborning = Airborning;
        Airborning = true;

        // Check if the player touches the ground again
        Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundPoint.position, mGroundedRadius, GroundLayer);
        foreach (var collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                Airborning = false;
                animator.SetBool("Jumping", false);

                // Fire event when the player grounds
                if (!wasAirborning)
                    OnLandEvent.Invoke();
            }
        }
    }

    #endregion

    #region Public Methods

    public override void JumpButton()
    {
        // Check if not already in the air
        if (!Airborning)
        {
            Airborning = true;

            // Add vertical force to the player
            mRigidbody2D.AddForce(new Vector2(0f, JumpForce));

            animator.SetBool("Jumping", true);
        }
    }

    /// <summary>
    /// Moves the character by walking or jumping
    /// </summary>
    /// <param name="move"></param>
    /// <param name="crouch"></param>
    /// <param name="jump"></param>
    public override void HorinzontalAxis(Vector2 move, bool Run = false, bool crouch = false)
    {
        TargetVelocity = Vector2.zero;

        if(crouch && CrouchControl)
        {
            if (!mCrouched)
            {
                mCrouched = true;
                animator.SetBool("Crouching", true);
            }
            else
            {
                mCrouched = false;
                animator.SetBool("Crouching", false);
            }

            move.x *= CrouchSpeed;
            
            // Movement while crouching
            TargetVelocity = new Vector2(move.x * 10f, mRigidbody2D.velocity.y);
        }
        
        if(!crouch)
        {
            // Calculate the speed
            move.x *= (Run ? RunSpeed : WalkSpeed);
            
            // Normal movement
            TargetVelocity = new Vector2(move.x * 10f, mRigidbody2D.velocity.y);
        }

        // If player is crouching, check if the character could stand up
        //if (!crouch)
        //    // If character has a ceiling preventing them from standing up, keep crouched
        //    if (Physics2D.OverlapCircle(CeilingPoint.position, mCeilingRadius, GroundLayer))
        //        crouch = true;

        // Face the player to the right side depending on his movement
        if (move.x > 0 && !mFacingRight)
            Flip();
        else if (move.x < 0 && mFacingRight)
            Flip();

        // Set speed animation parameter
        animator.SetFloat("Speed", Mathf.Abs(TargetVelocity.x));

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
        Airborning = true;
        mRigidbody2D.AddForce(new Vector2(0f, JumpForce));
    }

    #endregion

    public void OnAttackEvent(bool attacking) => Attacking = attacking;

    #region Private Methods

    private void CrouchController(Vector2 move, bool crouch, bool jump)
    {
        if (!Airborning)
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
