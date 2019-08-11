using UnityEngine;

/// <summary>
/// Controls the death land state for the player controller
/// </summary>
public class DyingLandSMB : SceneSMB<KongController>
{
    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.SetVelocity(Vector2.zero);
    }
}
