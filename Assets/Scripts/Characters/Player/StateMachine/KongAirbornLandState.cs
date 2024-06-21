using UnityEngine;

public class KongAirbornLandState : BaseState<KongController>
{
    #region Constructor

    public KongAirbornLandState(KongController controller, Animator animator) : base(controller, animator) { }

    #endregion

    #region State Events

    public override void OnStateStart()
    {
        controller.BounceDamager.enabled = false;

        animator.Play(KongController.Animations.Land);
    }

    public override void OnStateFixedUpdate()
    {
        controller.PerformVelocityHorizontalMovement(KongController.Instance.MovementSettings.WalkSpeed);
    }

    #endregion
}