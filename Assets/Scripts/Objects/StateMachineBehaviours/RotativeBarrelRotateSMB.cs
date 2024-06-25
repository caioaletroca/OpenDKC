using UnityEngine;

/// <summary>
/// Controls the player controllable rotative state for the rotative barrel
/// </summary>
public class RotativeBarrelRotateSMB : SceneSMB<RotativeBarrel>
{
    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.PerformBlastTimer();
        mMonoBehaviour.UpdateDirection();
        mMonoBehaviour.UpdateBlastDiretion();

        // Check for state change
        if (mMonoBehaviour.CheckForBlastTimer())
            mMonoBehaviour.PerformAutoBlast();
    }
}
