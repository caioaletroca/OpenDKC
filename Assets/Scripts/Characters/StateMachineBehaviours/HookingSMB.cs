using UnityEngine;
/// <summary>
/// The hook state for the player controller
/// </summary>
public class HookingSMB : SceneSMB<KongController>
{

    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Only for debugging purposes
        mMonoBehaviour.SetLocalPosition(mMonoBehaviour.HookSettings.SnapOffset);
    }

    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.EnableGravity();
        mMonoBehaviour.SetParent(null);
    }
}
