public partial class KongController {
    #region Events

    public void OnBounceDamageableHit(Damager damager, Damageable damageable) {
        Somersault = true;

        VFXController.Instance.Trigger("CrashVFX", transform.position);
    }

    #endregion
}