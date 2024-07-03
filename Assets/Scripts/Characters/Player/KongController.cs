using System;
using UnityEngine;

/// <summary>
/// The player controller, handling all physics movements and animations for the Kong
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
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
    /// Instance for the knock back system
    /// </summary>
    public InteractOnCollision2D KnockBackController;

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

    [HideInInspector]
    public KongStateMachine stateMachine;

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

    /// <summary>
    /// Instance for the Throw Settings
    /// </summary>
    public ThrowSettings ThrowSettings;

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
        stateMachine = new KongStateMachine(this, animator);
        SceneSMB<KongController>.Initialise(animator, this);
    }

    private void Start()
    {
        RegisterStateVariables();
    }

    public void Update() {
        stateMachine.OnStateUpdate();
    }

    public void FixedUpdate()
    {
        UpdateStateVariables();

        stateMachine.OnStateFixedUpdate();
        
        UpdateVelocityHorizontalMovement();
        UpdateForceHorizontalMovement();
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

        // Disable knock back system
        KnockBackController.enabled = false;

        // Plays death sound
        BackgroundMusicPlayer.Instance.PushClip(DamagerSettings.DeathMusic);

        // Reduce life count
        LifeController.LifeCount--;

        // Calls delayed restart
        SceneController.RestartDelay(DamagerSettings.RestartDelay);

        // Disable other interactions
        DisableEnemyInteraction();

        // TODO: Make it change between diddy and dixie
        VFXController.Instance.Trigger("DiddyHitSFX", transform.position);
    }

    /// <summary>
    /// Fired when the player falls to death
    /// </summary>
    public void OnFallDieEvent()
    {
        // Set variable state
        Die = true;

        // TODO: Adjust fall death

        // Plays death sound
        BackgroundMusicPlayer.Instance.PushClip(DamagerSettings.DeathMusic);

        // Reduce life count
        LifeController.LifeCount--;

        // Calls delayed restart
        SceneController.RestartDelay(DamagerSettings.RestartDelay);
    }

    /// <summary>
    /// Fired when the player gets knock back
    /// </summary>
    /// <param name="collision"></param>
    public void OnKnockBackEvent(Collision2D collision)
    {
        // Cancel attack movement
        Attack = false;
        
        // Calculate the direction vector
        var collisionDirection = (transform.position - collision.gameObject.transform.position).normalized;

        // Stops current velocity
        mRigidBody2D.velocity = Vector2.zero;

        // Add knock back force
        mRigidBody2D.AddForce(new Vector2(MovementSettings.KnockBackForce.x * collisionDirection.x, MovementSettings.KnockBackForce.y), ForceMode2D.Impulse);
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
    /// Set a new local position using <see cref="Rigidbody2D"/>
    /// </summary>
    /// <param name="position">The new position</param>
    public void SetLocalPosition(Vector3 position)
    {
        // Preserve Z position
        var newPosition = new Vector3(position.x, position.y, transform.position.z);

        // Set local position
        if (transform.parent != null)
            mRigidBody2D.MovePosition(transform.parent.TransformPoint(newPosition));
        else
            mRigidBody2D.MovePosition(transform.TransformPoint(newPosition));
    }

    /// <summary>
    /// Sets a new velocity for the <see cref="Rigidbody2D"/>
    /// </summary>
    /// <param name="velocity">The new velocity</param>
    public void SetVelocity(Vector2 velocity) => mRigidBody2D.velocity = velocity;

    /// <summary>
    /// Enables the physics for the <see cref="Rigidbody2D"/>
    /// </summary>
    public void EnableGravity() => mRigidBody2D.gravityScale = 3;

    /// <summary>
    /// Disables the physics for the <see cref="Rigidbody2D"/>
    /// </summary>
    public void DisableGravity() => mRigidBody2D.gravityScale = 0;

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

        // Call Animation trigger to handle the walking/running flipping animation
        FlipTrigger();
    }

    #endregion
}
