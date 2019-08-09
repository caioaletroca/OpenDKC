using UnityEngine;

/// <summary>
/// Controls the neek enemy
/// </summary>
[RequireComponent(typeof(PlatformMovement))]
public class Neek : Enemy
{
    #region Private Properties

    /// <summary>
    /// Instance for the platform movement
    /// </summary>
    protected PlatformMovement platformMovement;

    #endregion

    #region Unity Methdos

    protected new void Awake()
    {
        base.Awake();

        platformMovement = GetComponent<PlatformMovement>();
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

    public override void OnActivateEvent()
    {
        base.OnActivateEvent();

        platformMovement.enabled = true;
    }

    public override void OnDeactivateEvent()
    {
        base.OnDeactivateEvent();

        platformMovement.enabled = false;
    }

    #endregion
}
