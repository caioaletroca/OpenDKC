using UnityEngine;

public class KongHookUnhookingState : BaseState<KongController>
{
    #region Constructor

    public KongHookUnhookingState(KongController controller, Animator animator) : base(controller, animator) { }

    #endregion

    #region State Events

    public override void OnStateStart()
    {
        controller.PerformHookDismount();

        animator.Play(KongController.Animations.Rise);
    }

    public override void OnStateFixedUpdate()
    {
        controller.PerformVelocityHorizontalMovement(KongController.Instance.MovementSettings.WalkSpeed);
    }

    #endregion
}