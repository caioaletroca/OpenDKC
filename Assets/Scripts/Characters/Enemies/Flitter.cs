using UnityEngine;
/// <summary>
/// Controls the flitter enemy
/// </summary>
public class Flitter : Enemy
{
    #region Public Properties

    /// <summary>
    /// A instance for the damager class
    /// </summary>
    public Damager Damager;

    /// <summary>
    /// The amount of time to despawn the object after death
    /// </summary>
    [Tooltip("The amount of time to despawn the object after death.")]
    public float TimeToDespawn = 5;

    #endregion

    #region Unity Methods

    private new void Awake()
    {
        base.Awake();

        // Enables damager
        Damager.Enable();
    }

    #endregion

    #region Event Methods

    public override void OnDieEvent(Damager damager, Damageable damageable)
    {
        // Set state variable
        Die = true;

        // Disables damager
        Damager.Disable();

        // Resets velocity
        mRigidBody2D.velocity = Vector2.zero;
        mRigidBody2D.gravityScale = 1;

        // Perform Death jump
        //mRigidBody2D.AddForce(new Vector2(DeathJumpForce.x * damageable.DamageDirection.x, DeathJumpForce.y), ForceMode2D.Impulse);

        // Disables all colliders
        foreach (var collider in colliders)
            collider.enabled = false;

        // Despawn in time
        Destroy(gameObject, TimeToDespawn);
    }

    #endregion
}
