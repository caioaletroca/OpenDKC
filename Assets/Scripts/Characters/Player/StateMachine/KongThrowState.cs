using UnityEngine;

public class KongThrowState : BaseState<KongController>
{
    #region Constructor

    public KongThrowState(KongController controller, Animator animator) : base(controller, animator) { }

    #endregion

    #region State Events

    public override void OnStateStart()
    {
        controller.PerformItemThrow();
        controller.PerformVelocityHorizontalMovement(0);

        animator.Play(KongController.Animations.Throw);
    }

    public override void OnStateFixedUpdate()
    {
        controller.PerformVelocityHorizontalMovement(0);
    }

    #endregion
}