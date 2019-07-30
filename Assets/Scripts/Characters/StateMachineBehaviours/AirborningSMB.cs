using UnityEngine;

public class AirborningSMB : SceneSMB<KongController>
{
    #region State Methods

    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.PerformVelocityHorizontalMovement(
            mMonoBehaviour.Run ?
            mMonoBehaviour.MovementSettings.RunSpeed :
            mMonoBehaviour.MovementSettings.WalkSpeed
        );
    }

    #endregion
}
