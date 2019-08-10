using UnityEngine;

/// <summary>
/// Dying state for the player controller
/// </summary>
public class DyingSMB : SceneSMB<KongController>
{
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.PerformVelocityHorizontalMovement(0);
        mMonoBehaviour.SetVelocity(Vector2.zero);
        mMonoBehaviour.PerformDeathJump();
    }
}
