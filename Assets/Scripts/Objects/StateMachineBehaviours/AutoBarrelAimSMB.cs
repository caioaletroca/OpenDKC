using UnityEngine;

/// <summary>
/// Controls the aim state for the auto barrel
/// </summary>
public class AutoBarrelAimSMB : DelayedSceneSMB<AutoBarrel>
{
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateEnter(animator, stateInfo, layerIndex);

        mMonoBehaviour.Direction = 0;
    }

    public override void OnSLStateEnterDelayed(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.PerformAimDirection();
    }

    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.UpdateBlastDiretion();
        if (mMonoBehaviour.CheckForAimFinish())
            mMonoBehaviour.PerformAutoBlast();
    }
}
