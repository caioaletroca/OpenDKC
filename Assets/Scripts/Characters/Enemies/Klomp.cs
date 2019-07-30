using UnityEngine;

/// <summary>
/// Controls the klomp enemy
/// </summary>
public class Klomp : Enemy
{
    #region Public Properties

    /// <summary>
    /// The walking speed for the object
    /// </summary>
    public float Speed = 5;

    /// <summary>
    /// The look direction for the cliff detection system
    /// </summary>
    [Tooltip("Look direction for the cliff detection system.")]
    public Vector2 LookDirection;

    /// <summary>
    /// The layers to check for the cliff detection
    /// </summary>
    [Tooltip("The layers to check for the cliff detection.")]
    public LayerMask GroundLayer;

    #endregion

    #region Private Properties

    /// <summary>
    /// Instance for the audio source
    /// </summary>
    protected AudioSource audioSource;

    #endregion

    #region Unity Methods

    private new void Awake()
    {
        base.Awake();

        audioSource = GetComponent<AudioSource>();

        // Enables damager
        Damager.Enable();
    }

    private void FixedUpdate()
    {
        // Deactivate movement if died
        if (Die)
            return;

        var direction = transform.localScale.x * -1;

        // Set speed
        mRigidBody2D.velocity = new Vector2(direction * Speed, mRigidBody2D.velocity.y);

        // Check if should turn on cliff
        var realDirection = new Vector2(LookDirection.x * transform.localScale.x, LookDirection.y);
        var hit = Physics2D.Raycast(transform.position, realDirection, LookDirection.magnitude, GroundLayer);
        if (hit.collider == null)
            Flip();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Controls the flip behaviour when hitting a wall
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
            Flip();
    }
   
    private void OnDrawGizmosSelected()
    {
        // Calculate points
        var realDirection = new Vector2(LookDirection.x * transform.localScale.x, LookDirection.y);
        var endPosition = realDirection + (Vector2)transform.position;

        // Draw gizmo
        Gizmos.DrawLine(transform.position, endPosition);
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

    #region Public Methods

    /// <summary>
    /// Fires when the animation hits the klomp step time
    /// </summary>
    public void OnStepEvent()
    {
        // Produces step sound
        audioSource.Play();
    }

    #endregion
}
