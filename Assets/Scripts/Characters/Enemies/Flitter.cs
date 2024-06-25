using UnityEngine;
/// <summary>
/// Controls the flitter enemy
/// </summary>
public class Flitter : Enemy
{
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
}
