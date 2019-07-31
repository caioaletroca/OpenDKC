using UnityEngine;

/// <summary>
/// Constrols the idle state for auto barrels
/// </summary>
public class AutoBarrelIdleSMB : SceneSMB<AutoBarrel>
{
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.PerformFrame(mMonoBehaviour.StartFrame);
        mMonoBehaviour.PerformBlastDirection(mMonoBehaviour.StartFrame);
    }
}
