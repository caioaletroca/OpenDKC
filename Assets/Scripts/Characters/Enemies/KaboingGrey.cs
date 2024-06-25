using UnityEngine;

/// <summary>
/// Controls the Kaboing Grey enemy
/// </summary>
public class KaboingGrey : Enemy
{
    #region State Variables

    /// <summary>
    /// The current ground distance evaluated right below the character
    /// </summary>
    public float GroundDistance
    {
        get => animator.GetFloat("GroundDistance");
        set => animator.SetFloat("GroundDistance", value);
    }

    /// <summary>
    /// The speed for the current animation
    /// </summary>
    public float AnimationSpeed
    {
        get => animator.GetFloat("AnimationSpeed");
        set => animator.SetFloat("AnimationSpeed", value);
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// The speed for the x movement
    /// </summary>
    [Tooltip("The speed for the x movement.")]
    public float Speed;

    /// <summary>
    /// The amount of force used on the jump movement
    /// </summary>
    [Tooltip("The amount of force used on the jump movement.")]
    public Vector2 JumpForce = new Vector2(1, 1);

    /// <summary>
    /// The layer mask to check against ground distance
    /// </summary>
    [Tooltip("The layer mask to check against ground distance.")]
    public LayerMask GroundLayer;

    #endregion

    #region Unity Methods

    protected new void Awake()
    {
        base.Awake();

        // Enable State machine
        SceneSMB<KaboingGrey>.Initialise(animator, this);
    }

    protected void Update()
    {
        UpdateGroundDistance();
        UpdateVerticalSpeed();
    }

    protected new void FixedUpdate()
    {
        base.FixedUpdate();
        
        // Do not update when grounded
        if (GroundDistance < 1)
            return;

        var direction = transform.localScale.x * -1;

        mRigidBody2D.velocity = new Vector2(direction * Speed, mRigidBody2D.velocity.y);
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        // Controls the flip behaviour when hitting a wall
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
            Flip();
    }

    #endregion

    #region Event Methods

    /// <summary>
    /// Fired when the enemy dies
    /// </summary>
    /// <param name="damager"></param>
    /// <param name="damageable"></param>
    public override void OnDieEvent(Damager damager, Damageable damageable)
    {
        // Set state variable
        Die = true;

        DisableEnemy();
        PerformDeathJump(damageable.DamageDirection);

        // Despawn in time
        Destroy(gameObject, TimeToDespawn);
    }

    #endregion

    #region Perform Methods

    /// <summary>
    /// Performs the jump movement
    /// </summary>
    public void PerformJump() => mRigidBody2D.AddForce(JumpForce, ForceMode2D.Impulse);

    #endregion

    #region Update Methods

    /// <summary>
    /// Updates the ground distance state variable
    /// </summary>
    public void UpdateGroundDistance()
    {
        // Calculate distance to the ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 100, GroundLayer);
        if (hit.collider != null)
            GroundDistance = hit.distance;
    }

    #endregion
}
