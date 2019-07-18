using UnityEngine;

/// <summary>
/// The Idle state, basically the player only stands still
/// </summary>
public class IdleSMB : SceneSMB<KongController>
{
    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Forces player to stand still in position
        mMonoBehaviour.PerformHorizontalMovement(0);
    }
}
