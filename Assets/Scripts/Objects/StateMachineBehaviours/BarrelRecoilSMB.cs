using UnityEngine;

public class BarrelRecoilSMB : SceneSMB<RotativeBarrel>
{
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.UpdateRecoilDirection();
    }

    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (mMonoBehaviour.CheckForRecoilFinish())
            mMonoBehaviour.PerformIdle();
    }
}
