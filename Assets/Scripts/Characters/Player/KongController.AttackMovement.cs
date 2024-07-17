using UnityEngine;

public partial class KongController
{
    #region Public Methods

    /// <summary>
    /// Perform a idle attack impulse
    /// </summary>
    public void PerformIdleAttack() => PerformHorizontalImpulse(FacingRight ? MovementSettings.AttackForce : -MovementSettings.AttackForce);

    #endregion
    
    #region Event Methods

    public void OnAttackDamageableHit(Damager damager, Damageable damageable) {
        VFXController.Instance.Trigger("CrashVFX", transform.position);
    }

    /// <summary>
    /// Fires when the attack animation finishes
    /// </summary>
    public void OnAttackAnimationFinished() => Attack = false;

    #endregion
}
