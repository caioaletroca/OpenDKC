using UnityEngine;

/// <summary>
/// Handles the transition state to the hook state for the player
/// </summary>
public class HookingEnterSMB : SceneSMB<KongController>
{
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.PerformHorizontalMovement(0);
        mMonoBehaviour.DisableRigidBody();
        mMonoBehaviour.SetLocalPosition(mMonoBehaviour.HookSettings.SnapOffset);
    }
}
