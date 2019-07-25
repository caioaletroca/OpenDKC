using UnityEngine;

/// <summary>
/// The rising state for the player controller
/// </summary>
public class AirborningRiseSMB : SceneSMB<KongController>
{
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.PerformJump();
    }
}
