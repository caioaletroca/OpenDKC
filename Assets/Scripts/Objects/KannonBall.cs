/// <summary>
/// Controls the Kannon Ball projetile object
/// </summary>
public class KannonBall : Bullet
{
    #region Event Methods

    public void OnDamageEvent(Damager damager, Damageable damageable)
    {
        VFXController.Instance.Trigger("CrashLandXF", damageable.transform.position);
    }

    #endregion
}
