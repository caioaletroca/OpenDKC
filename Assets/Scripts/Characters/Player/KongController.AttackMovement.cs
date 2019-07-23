using UnityEngine;

public partial class KongController
{
    #region Public Methods

    /// <summary>
    /// Fires when the attack animation finishes
    /// </summary>
    public void OnAttackAnimationFinished() => Attack = false;

    #endregion
}
