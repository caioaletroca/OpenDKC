using UnityEngine;

public class KongJumpHoldState : BaseState<KongController>
{
    #region Constructor

    public KongJumpHoldState(KongController controller, Animator animator) : base(controller, animator) { }

    #endregion

    #region Public Methods

    public override void RegisterTransitions(BaseStateMachine<KongController> stateMachine)
    {
        var idleHold = stateMachine.GetState(typeof(KongIdleHoldState));
        var walkHold = stateMachine.GetState(typeof(KongWalkHoldState));
        var throwing = stateMachine.GetState(typeof(KongThrowState));

        AddTransition(idleHold, new FunctionPredicate(() => controller.Grounded && controller.HorizontalValue < 0.001));
        AddTransition(walkHold, new FunctionPredicate(() => controller.Grounded && controller.HorizontalValue > 0.001));
        AddTransition(throwing, new FunctionPredicate(() => !controller.Hold && controller.VerticalValue > -0.5));
    }

    #endregion

    #region State Events

    public override void OnStateStart()
    {
        controller.PerformJump();

        animator.Play(KongController.Animations.IdleHold);
    }

    public override void OnStateFixedUpdate()
    {
        controller.PerformVelocityHorizontalMovement(KongController.Instance.MovementSettings.RunSpeed);
    }

    #endregion
}