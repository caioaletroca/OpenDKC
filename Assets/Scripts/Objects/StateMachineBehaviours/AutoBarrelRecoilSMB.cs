using UnityEngine;

/// <summary>
/// Controls the recoil state for the auto barrel
/// </summary>
public class AutoBarrelRecoilSMB : DelayedSceneSMB<AutoBarrel>
{
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateEnter(animator, stateInfo, layerIndex);

        // Stops movement
        mMonoBehaviour.Direction = 0;
    }

    public override void OnSLStateEnterDelayed(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.PerformRecoilDirection();
    }

    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (mMonoBehaviour.CheckForRecoilFinish())
            mMonoBehaviour.PerformIdle();
    }   
}
