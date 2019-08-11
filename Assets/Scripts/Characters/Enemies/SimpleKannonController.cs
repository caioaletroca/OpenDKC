using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Controls a simple timed blast kannon
/// </summary>
public class SimpleKannonController : BulletPool
{
    #region Public Properties

    /// <summary>
    /// The time in seconds between the bullets
    /// </summary>
    [Tooltip("The time in seconds between the bullets.")]
    public float ShootTime = 5;

    /// <summary>
    /// The shoot velocity and direction
    /// </summary>
    [Tooltip("The shoot velocity and direction.")]
    public Vector2 ShootVelocity = new Vector2(1, 1);

    /// <summary>
    /// The position the bullet will be spawn
    /// </summary>
    [Tooltip("The position the bullet will be spawn.")]
    public Transform ShootPosition;

    /// <summary>
    /// Fired when a shoot occurs
    /// </summary>
    [Tooltip("Fired when a shoot occurs.")]
    public UnityEvent OnShootEvent;

    #endregion

    #region Private Properties

    /// <summary>
    /// The internal value for the shoot timer
    /// </summary>
    protected float ShootTimer;

    #endregion

    #region Unity Methods

    protected void Awake()
    {
        ShootTimer = ShootTime;
    }

    protected void Update()
    {
        // Decrement Timer
        ShootTimer -= Time.deltaTime;

        if(ShootTimer <= 0)
        {
            // Reset timer
            ShootTimer = ShootTime;

            // Fire the bullet
            Shoot();
        }
    }

    private void OnEnable() => ShootTimer = ShootTime;

    protected void OnDrawGizmosSelected()
    {
        // Do not draw gizmo if not defined shoot position
        if (ShootPosition == null)
            return;
        
        // Calculate the direction
        var pointB = (Vector2)ShootPosition.position + ShootVelocity.normalized;

        // Draw the fire direction
        Gizmos.color = Color.yellow;
        Handles.Label(pointB, "Shoot Direction");
        Gizmos.DrawLine(ShootPosition.position, pointB);
        Gizmos.DrawWireSphere(pointB, 0.1f);
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Shoots a projectile
    /// </summary>
    public void Shoot()
    {
        // Get a new bullet
        var bullet = Pop(ShootPosition.position);

        // Set velocity
        bullet.rigidBody2D.velocity = ShootVelocity;

        // Fire event
        OnShootEvent?.Invoke();
    }

    #endregion
}
