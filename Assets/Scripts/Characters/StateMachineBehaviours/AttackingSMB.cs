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
        mMonoBehaviour.Damager.GetComponent<Collider>().enabled = true;
    }

    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.PerformHorizontalMovement(KongController.Instance.MovementSettings.RunSpeed);
    }

    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.Damager.GetComponent<Collider>().enabled = false;
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
