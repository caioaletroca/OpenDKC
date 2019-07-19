using UnityEngine;
/// <summary>
/// The hook state for the player controller
/// </summary>
public class HookingSMB : SceneSMB<KongController>
{
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.Teleport(mMonoBehaviour.HookGameObject.transform.position + mMonoBehaviour.HookSettings.SnapOffset);
        mMonoBehaviour.PerformHorizontalMovement(0);
    }

    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.Teleport(mMonoBehaviour.HookGameObject.transform.position + mMonoBehaviour.HookSettings.SnapOffset);
        mMonoBehaviour.PerformHorizontalMovement(0);
    }
}
