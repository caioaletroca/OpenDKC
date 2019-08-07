using UnityEngine;

/// <summary>
/// Controls the neek enemy
/// </summary>
public class Neek : Enemy
{
    #region Public Properties

    /// <summary>
    /// The walking speed for the object
    /// </summary>
    public float Speed = 5;

    #endregion

    #region Unity Methods

    protected void FixedUpdate()
    {  
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
}
