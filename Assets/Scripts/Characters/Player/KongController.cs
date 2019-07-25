using System;
using UnityEngine;

/// <summary>
/// The player controller, handling all physics movements and animations for the Kong
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public partial class KongController : MonoBehaviour
{
    #region Singleton

    /// <summary>
    /// A singleton instance for the <see cref="KongController"/>
    /// </summary>
    [NonSerialized]
    public static KongController Instance = null;

    #endregion

    #region Public Properties

    /// <summary>
    /// Instance for the Damager controller
    /// </summary>
    public Damager Damager;

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
    /// Instance for the Damage Settings
    /// </summary>
    public DamageSettings DamagerSettings;

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
    }

    private void Start()
    {
        RegisterStateVariables();
    }

    public void FixedUpdate()
    {
        UpdateStateVariables();
        UpdateHorizontalMovement();
        UpdateVerticalMovement();
    }

    #endregion

    #region Events Methods

    /// <summary>
    /// Fired when the player dies
    /// </summary>
    public void OnDieEvent(Damager damager, Damageable damageable)
    {
        // Set variable state
        Die = true;

        // Set Direction
        DeathDirection = damageable.DamageDirection;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Sets a new parent to the player object
    /// </summary>
    /// <param name="parent">The parent game object</param>
    public void SetParent(GameObject parent) => transform.parent = parent.transform;

    /// <summary>
    /// Enables the physics for the <see cref="Rigidbody2D"/>
    /// </summary>
    public void EnableRigidBody() => mRigidBody2D.simulated = true;

    /// <summary>
    /// Disables the physics for the <see cref="Rigidbody2D"/>
    /// </summary>
    public void DisableRigidBody() => mRigidBody2D.simulated = false;

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
