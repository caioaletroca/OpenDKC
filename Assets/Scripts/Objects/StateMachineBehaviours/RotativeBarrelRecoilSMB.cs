using UnityEngine;

/// <summary>
/// Controls the recoils state for the rotative barrel
/// </summary>
public class RotativeBarrelRecoilSMB : DelayedSceneSMB<RotativeBarrel>
{
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateEnter(animator, stateInfo, layerIndex);

        mMonoBehaviour.Direction = 0;
    }

    public override void OnSLStateEnterDelayed(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.UpdateRecoilDirection();
    }

    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (mMonoBehaviour.CheckForRecoilFinish())
            mMonoBehaviour.PerformIdle();
    }
}
