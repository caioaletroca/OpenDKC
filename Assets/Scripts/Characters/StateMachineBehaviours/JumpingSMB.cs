using UnityEngine;

/// <summary>
/// The jumping state for the player controller
/// </summary>
public class JumpingSMB : SceneSMB<KongController>
{
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.PerformJump();
    }
}
