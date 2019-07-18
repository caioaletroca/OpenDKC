using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

/// <summary>
/// The player controller, handling all physics movements and animations for the Kong
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public partial class KongController : Player
{
    #region Singleton

    /// <summary>
    /// A singleton instance for the <see cref="KongController"/>
    /// </summary>
    [NonSerialized]
    public static KongController Instance = null;

    #endregion

    #region Private Properties

    /// <summary>
    /// Instance for the <see cref="Rigidbody2D"/>
    /// </summary>
    private Rigidbody2D mRigidBody2D;

    /// <summary>
    /// A reference to the <see cref="Animator"/> component
    /// </summary>
    private Animator animator;

    /// <summary>
    /// A flag that represents if the player is facing the right side
    /// </summary>
    private bool mFacingRight = true;

    #endregion

    #region Settings

    /// <summary>
    /// Instance for the Movement Settings
    /// </summary>
    public MovementSettings MovementSettings;

    /// <summary>
    /// Instance for the Damager Settings
    /// </summary>
    public DamagerSettings DamagerSettings;

    /// <summary>
    /// Instance for the Hook Settings
    /// </summary>
    public HookSettings HookSettings;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        // Instantiate components
        mRigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Test if has instance for all Settings
        if (MovementSettings == null)
            Debug.Break();

        // Start the state machine
        SceneSMB<KongController>.Initialise(animator, this);

        RegisterStateVariables();
    }

    private void Update()
    {
        //if (PlayerInput.Instance.Pause)
        //{
        //    //Display pause screen
        //    Debug.Log("Game paused");

        //    PlayerInput.Instance.KongMap.Disable();
        //    PlayerInput.Instance.KongMap.Pause.Enable();

        //    //Freeze the current time
        //    Time.timeScale = 0;
        //}
        //else
        //    Unpause();
    }

    public void FixedUpdate()
    {
        UpdateStateVariables();
        UpdateHorizontalMovement();
        UpdateVerticalMovement();
        //UpdateRunState();
        //UpdateAttackState();
        //UpdateJumpState();
    }

    #endregion

    #region Public Methods

    public void Unpause()
    {
        Debug.Log("Game unpaused");
        
        // If the game has continued already
        if (Time.timeScale > 0)
            return;
    }

    public IEnumerator UnpauseCoroutine()
    {
        // Resets the time scale
        Time.timeScale = 1;

        // Reenables inputs
        PlayerInput.Instance.KongMap.Enable();

        // We have to wait for a fixed update so the pause button state change, otherwise we can get in case were the update
        // of this script happen BEFORE the input is updated, leading to setting the game in pause once again
        yield return new WaitForFixedUpdate();
        yield return new WaitForEndOfFrame();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Hook")
        {
            Hook = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hook")
        {
            Hook = false;
        }
    }

    #endregion

    #region Action Methods

    public override void TakeDamage()
    {
        Debug.Log("Player take damage");

        Destroy(gameObject, 1);

        GameManager.Instance.EndGame();
    }

    #endregion

    #region Private Methods

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
    }

    #endregion
}
