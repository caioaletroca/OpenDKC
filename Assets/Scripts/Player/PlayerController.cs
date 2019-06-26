using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : Player
{
    #region Types

    /// <summary>
    /// The State type of the character
    /// </summary>
    public enum State { Normal, Hook };

    #endregion

    #region Public Properties

    /// <summary>
    /// A singleton instance for the <see cref="PlayerController"/>
    /// </summary>
    [NonSerialized]
    public static PlayerController Instance = null;

    /// <summary>
    /// the current state as <see cref="State"/>
    /// </summary>
    [NonSerialized]
    public State CurrentState;

    /// <summary>
    /// A reference to the run controller
    /// </summary>
    public RunController runController;

    /// <summary>
    /// A referente to the hook controller
    /// </summary>
    public HookController hookController;

    /// <summary>
    /// A reference to the <see cref="Animator"/> component
    /// </summary>
    public Animator animator;

    /// <summary>
    /// A flag that represents jump
    /// </summary>
    [HideInInspector]
    public bool Jump = false;

    /// <summary>
    /// A flag that represents if the character is crouching
    /// </summary>
    [HideInInspector]
    public bool Crouch = false;

    /// <summary>
    /// A flag that represents if the character is running
    /// </summary>
    [HideInInspector]
    public bool Run = false;

    [HideInInspector]
    public bool Attack = false;

    #endregion

    #region Private Properties

    [SerializeField]
    private Rigidbody2D mRigidBody2D;

    /// <summary>
    /// Stores the horizontal movement velocity
    /// </summary>
    private Vector2 Movement = Vector2.zero;

    /// <summary>
    /// Stores the attack state
    /// </summary>
    private bool AttackLastState = false;

    #endregion

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        mRigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Handle jump input
        if (Input.GetButtonDown("Jump") && Jump == false)
        {
            Jump = true;
            animator.SetBool("Jumping", true);
        }

        // Handle Crouch inputs
        if (Input.GetButtonDown("Crouch"))
            Crouch = true;
        else if (Input.GetButtonUp("Crouch"))
            Crouch = false;

        // Handle attack inputs
        if (Input.GetButtonDown("Fire1"))
            Run = true;
        if (Input.GetButtonUp("Fire1"))
            Run = false;

        // If the attack button is pressed
        if (Input.GetButtonDown("Fire1") && !AttackLastState)
        {
            animator.SetBool("Attack", true);
            animator.SetBool("Run", true);

            AttackLastState = true;
        }
        else
        {
            animator.SetBool("Attack", false);
            AttackLastState = false;
        }

        if (Input.GetButtonUp("Fire1"))
            animator.SetBool("Run", false);

        // Check for fall animation
        if (mRigidBody2D.velocity.y < 0 && !Jump)
        {
            animator.SetBool("Jumping", true);
        }

        // Calculate the movement
        Movement = new Vector2(Input.GetAxisRaw("Horizontal"), 0);

        // Set animator
        animator.SetFloat("Speed", Mathf.Abs(Movement.x));
    }

    private void FixedUpdate()
    {
        if (CurrentState == State.Normal)
            runController.Move(Movement * Time.fixedDeltaTime, Run, Crouch, Jump);
        else if (CurrentState == State.Hook)
            hookController.Move(Movement * Time.fixedDeltaTime, Run, Crouch, Jump);
    }

    #region Public Methods

    public override void TakeDamage()
    {
        Debug.Log("Player take damage");

        Destroy(gameObject, 1);

        GameManager.Instance.EndGame();
    }

    public override void JumpEnemy() => runController.JumpEnemy();

    #endregion

    #region Events Methods

    public void OnGroundDistanceEvent(float GroundDistance) => animator.SetFloat("GroundDistance", GroundDistance);

    public void OnLandEvent()
    {
        animator.SetBool("Jumping", false);
        Jump = false;
    }

    public void OnCrouching(bool Crouching) => animator.SetBool("Crouching", Crouching);

    public void OnTurn(bool FacingRight) => animator.SetBool("FacingRight", FacingRight);

    #endregion
}
