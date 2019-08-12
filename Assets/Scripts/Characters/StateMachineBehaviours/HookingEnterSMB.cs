using UnityEngine;

/// <summary>
/// Handles the transition state to the hook state for the player
/// </summary>
public class HookingEnterSMB : SceneSMB<KongController>
{
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.PerformVelocityHorizontalMovement(0);
        mMonoBehaviour.DisableGravity();
        mMonoBehaviour.SetVelocity(Vector2.zero);
        mMonoBehaviour.SetLocalPosition(mMonoBehaviour.HookSettings.SnapOffset);
    }
}
