/// <summary>
/// Handles the barrel projectile from kannon
/// </summary>
public class KannonBarrel : KannonBall
{
    #region Event Methods

    public void OnDamageableEvent(Damager damager, Damageable damageable)
    {
        bulletPoolObject.ReturnToPool();
    }

    #endregion
}