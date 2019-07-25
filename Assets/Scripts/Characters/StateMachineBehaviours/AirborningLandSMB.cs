using UnityEngine;

/// <summary>
/// The landing state for the player controller
/// </summary>
public class AirborningLandSMB : SceneSMB<KongController>
{
    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.BounceDamager.Disable();
    }
}
