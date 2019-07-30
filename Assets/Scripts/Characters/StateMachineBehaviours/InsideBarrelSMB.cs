using UnityEngine;
using UnityEngine.Animations;

public class InsideBarrelSMB : SceneSMB<KongController>
{
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.PerformHorizontalMovement(0);
        mMonoBehaviour.DisableRigidBody();
        mMonoBehaviour.SetLocalPosition(Vector2.zero);
    }

    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        mMonoBehaviour.EnableRigidBody();
    }
}
