using UnityEngine;

/// <summary>
/// The rising state for the kaboing grey
/// </summary>
public class KaboingGreyRisingSMB : DelayedSceneSMB<KaboingGrey>
{
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateEnter(animator, stateInfo, layerIndex);

        mMonoBehaviour.AnimationSpeed = 0;
    }

    public override void OnSLStateEnterDelayed(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.AnimationSpeed = 1;
        mMonoBehaviour.PerformJump();
    }
}
