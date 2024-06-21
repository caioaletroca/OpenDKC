using UnityEngine;

/// <summary>
/// Handles the transition state to the hook state for the player
/// </summary>
public class HookingDismountSMB : SceneSMB<KongController>
{
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.PerformHookDismount();
    }
}
