using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Defines a base class for all enemies
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(ProximityActivator))]
public class Enemy : MonoBehaviour
{
    #region State Variables

    /// <summary>
    /// Represents the object vertical speed
    /// </summary>
    public float VerticalSpeed
    {
        get => animator.GetFloat("VerticalSpeed");
        set => animator.SetFloat("VerticalSpeed", value);
    }

    /// <summary>
    /// Represents if the object has died
    /// </summary>
    public bool Die
    {
        get => animator.GetBool("Die");
        set {
            // If the enemy died, we should disable the proximity activator
            if(value == true) {
                proximityActivator.SoftActive = false;
            }

            animator.SetBool("Die", value);
        }
    }

    /// <summary>
    /// Animation trigger for flip sprite
    /// </summary>
    public void FlipTrigger() => animator.SetTrigger("FlipTrigger");

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
    /// The proximity activator instance
    /// </summary>
    [HideInInspector]
    public ProximityActivator proximityActivator;

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
        proximityActivator = GetComponent<ProximityActivator>();
    }

    public void FixedUpdate() {
        UpdateStateVariables();
    }

    #endregion

    #region Event Methods

    /// <summary>
    /// Fired when the enemy dies
    /// </summary>
    /// <param name="damager"></param>
    /// <param name="damageable"></param>
    public virtual void OnDieEvent(Damager damager, Damageable damageable) { }

    /// <summary>
    /// Fired when the enemy hits damage on player
    /// </summary>
    /// <param name="damager"></param>
    /// <param name="damageable"></param>
    public virtual void OnDamageEvent(Damager damager, Damageable damageable) {
        VFXController.Instance.Trigger("CrashVFX", damageable.transform.position);
    }

    /// <summary>
    /// Fired when the enemy is activated
    /// </summary>
    public virtual void OnActivateEvent()
    {
        animator.enabled = true;
    }

    /// <summary>
    /// Fired when the enemy is deactivated
    /// </summary>
    public virtual void OnDeactivateEvent()
    {
        animator.Play("start", -1, 0);
        animator.enabled = false;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Disable collisions and damagers
    /// </summary>
    public void DisableEnemy()
    {
        // Disables damager
        Damager.enabled = false;

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

    protected void UpdateStateVariables() {
        UpdateVerticalSpeed();
    }

    protected void UpdateVerticalSpeed() {
        VerticalSpeed = mRigidBody2D.velocity.y;
    }

    /// <summary>
    /// Flips the sprite for the object
    /// </summary>
    protected void Flip()
    {
        // Multiply the x local scale by -1
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        FlipTrigger();
    }

    #endregion
}
