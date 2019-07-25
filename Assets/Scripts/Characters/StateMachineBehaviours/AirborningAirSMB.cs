using UnityEngine;

/// <summary>
/// The airborning state for the player controller
/// </summary>
public class AirborningAirSMB : SceneSMB<KongController>
{
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.BounceDamager.Enable();
    }
}
