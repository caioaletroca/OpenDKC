using UnityEngine;

/// <summary>
/// Constrols the idle state for barrels
/// </summary>
public class RotativeBarrelIdleSMB : SceneSMB<RotativeBarrel>
{
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.PerformFrame(mMonoBehaviour.StartFrame);
        mMonoBehaviour.PerformBlastDirection(mMonoBehaviour.StartFrame);
    }
}
