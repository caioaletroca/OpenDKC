using UnityEngine;

/// <summary>
/// Inside invisible barrel state for the player controller
/// </summary>
public class InsideBarrelSMB : SceneSMB<KongController>
{
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.SetVelocity(Vector2.zero);
        mMonoBehaviour.DisableGravity();
        mMonoBehaviour.SetLocalPosition(Vector2.zero);
    }
}
