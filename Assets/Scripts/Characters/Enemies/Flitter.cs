using UnityEngine;
/// <summary>
/// Controls the flitter enemy
/// </summary>
public class Flitter : Enemy
{
    #region State Variables

    /// <summary>
    /// The vertical speed state variable
    /// </summary>
    [HideInInspector]
    public float VerticalSpeed
    {
        get => animator.GetFloat("VerticalSpeed");
        set => animator.SetFloat("VerticalSpeed", value);
    }

    #endregion

    #region Unity Methods

    protected void Update()
    {
        UpdateVerticalSpeed();
    }

    #endregion

    #region Event Methods

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

    #region Private Methods

    /// <summary>
    /// Updates the vertical speed variable
    /// </summary>
    protected void UpdateVerticalSpeed()
    {
        // Do not update if alive
        if (!Die)
            return;

        // Update state variable
        VerticalSpeed = mRigidBody2D.velocity.y;
    }

    #endregion
}
