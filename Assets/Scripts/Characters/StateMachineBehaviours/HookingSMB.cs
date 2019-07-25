using UnityEngine;
/// <summary>
/// The hook state for the player controller
/// </summary>
public class HookingSMB : SceneSMB<KongController>
{
    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.EnableRigidBody();
        mMonoBehaviour.SetParent(null);
    }
}
