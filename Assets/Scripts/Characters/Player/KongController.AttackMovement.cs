using UnityEngine;

public partial class KongController
{
    #region Events

    public void OnAttackDamageableHit(Damager damager, Damageable damageable) {
        VFXController.Instance.Trigger("CrashVFX", transform.position);
    }

    /// <summary>
    /// Fires when the attack animation finishes
    /// </summary>
    public void OnAttackAnimationFinished() => Attack = false;

    #endregion
}
