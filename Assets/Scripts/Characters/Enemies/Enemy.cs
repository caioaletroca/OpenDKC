using UnityEngine;

/// <summary>
/// Defines a base class for all enemies
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    #region State Variables

    /// <summary>
    /// Represents if the object has died
    /// </summary>
    [HideInInspector]
    public bool Die
    {
        get => animator.GetBool("Die");
        set => animator.SetBool("Die", value);
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// A instance for the damager class
    /// </summary>
    public Damager Damager;

    /// <summary>
    /// The direction for the death jump effect
    /// </summary>
    [Tooltip("The force applied when the object dies.")]
    public Vector2 DeathJumpForce = new Vector2(0, 200);

    /// <summary>
    /// The amount of time to despawn the object after death
    /// </summary>
    [Tooltip("The amount of time to despawn the object after death.")]
    public float TimeToDespawn = 5;

    /// <summary>
    /// The persistence data configuration
    /// </summary>
    [HideInInspector]
    public DataSettings dataSettings;

    #endregion

    #region Private Properties

    /// <summary>
    /// A instance for the <see cref="Rigidbody2D"/> for the game Object
    /// </summary>
    protected Rigidbody2D mRigidBody2D;

    /// <summary>
    /// A instance for the <see cref="Animator"/> for the game Object
    /// </summary>
    protected Animator animator;

    /// <summary>
    /// A instance for all the <see cref="Collider2D"/> for the game Object
    /// </summary>
    protected Collider2D[] colliders;

    #endregion 

    #region Unity Methods

    protected void Awake()
    {
        mRigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        colliders = GetComponents<Collider2D>();
    }

    #endregion

    #region Event Methods

    /// <summary>
    /// Fired when the enemy dies
    /// </summary>
    /// <param name="damager"></param>
    /// <param name="damageable"></param>
    public virtual void OnDieEvent(Damager damager, Damageable damageable) { }

    #endregion

    #region Public Methods

    /// <summary>
    /// Disable collisions and damagers
    /// </summary>
    public void DisableEnemy()
    {
        // Disables damager
        Damager.Disable();

        // Disables all colliders
        foreach (var collider in colliders)
            collider.enabled = false;
    }

    /// <summary>
    /// Performs the death jump for enemy
    /// </summary>
    public void PerformDeathJump(Vector2 damageDirection)
    {
        // Resets velocity
        mRigidBody2D.velocity = Vector2.zero;

        // Turn gravity on
        mRigidBody2D.gravityScale = 1;

        // Perform Death jump
        mRigidBody2D.AddForce(new Vector2(DeathJumpForce.x * damageDirection.x, DeathJumpForce.y), ForceMode2D.Impulse);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Flips the sprite for the object
    /// </summary>
    protected void Flip()
    {
        // Multiply the x local scale by -1
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    #endregion
}
