using UnityEngine;

/// <summary>
/// The crouching state for the player controller
/// </summary>
public class CrouchSMB : SceneSMB<KongController>
{
    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.PerformHorizontalMovement(KongController.Instance.MovementSettings.CrouchSpeed);
    }
}
