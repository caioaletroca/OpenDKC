using System;
using UnityEngine;

/// <summary>
/// The player controller, handling all physics movements and animations for the Kong
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[DisallowMultipleComponent]
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

    [Header("Controller Instances", order = 0)]

    /// <summary>
    /// Instance for the normal Attack controller
    /// </summary>
    public Damager AttackDamager;

    /// <summary>
    /// Instance for the bounce controller
    /// </summary>
    public Damager BounceDamager;

    /// <summary>
    /// Instance for the damageable controller
    /// </summary>
    public Damageable Damageable;

    /// <summary>
    /// Instance for the life controller
    /// </summary>
    public LifeController LifeController;

    /// <summary>
    /// Instance for the banana controller
    /// </summary>
    public BananaController BananaController;

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

    [Header("Settings", order = 1)]

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
        // Renew instance
        Instance = this;

        // Instantiate components
        mRigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Test if has instance for all Settings
        if (MovementSettings == null || DamagerSettings == null || HookSettings == null)
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

        // Plays death sound
        BackgroundMusicPlayer.Instance.PushClip(DamagerSettings.DeathMusic);

        // Calls delayed restart
        SceneController.RestartDelay(DamagerSettings.RestartDelay);
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Sets a new parent to the player object
    /// </summary>
    /// <param name="parent">The parent game object</param>
    public void SetParent(GameObject parent)
    {
        if (parent == null)
            transform.parent = null;
        else
            transform.parent = parent.transform;
    }

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
