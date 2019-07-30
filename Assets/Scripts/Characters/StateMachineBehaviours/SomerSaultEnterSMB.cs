using UnityEngine;

/// <summary>
/// Handles the somer sault state for the player controller
/// </summary>
public class SomerSaultEnterSMB : SceneSMB<KongController>
{
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.PerformJump();
    }
}
