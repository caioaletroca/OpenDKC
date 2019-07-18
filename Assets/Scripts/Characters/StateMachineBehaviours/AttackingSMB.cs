using System.Collections;
using UnityEngine;

/// <summary>
/// The attacking state for the player controller
/// </summary>
public class AttackingSMB : SceneSMB<KongController>
{
    #region State Methods

    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.StartCoroutine(EndAttack());
    }

    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.PerformHorizontalMovement(KongController.Instance.MovementSettings.RunSpeed);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Exits the attack state after the duration time
    /// </summary>
    /// <returns></returns>
    protected IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(mMonoBehaviour.DamagerSettings.AttackDuration);
        
        mMonoBehaviour.Attack = false;

        yield return null;
    }

    #endregion
}
