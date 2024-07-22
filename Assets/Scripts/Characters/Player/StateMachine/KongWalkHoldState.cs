using UnityEngine;

public class KongWalkHoldState : BaseState<KongController>
{
    #region Constructor

    public KongWalkHoldState(KongController controller, Animator animator) : base(controller, animator) { }

    #endregion

    #region Public Methods

    public override void RegisterTransitions(BaseStateMachine<KongController> stateMachine)
    {
        var idleHold = stateMachine.GetState(typeof(KongIdleHoldState));
        var dropping = stateMachine.GetState(typeof(KongDroppingState));
        var throwing = stateMachine.GetState(typeof(KongThrowState));
        var jumpHold = stateMachine.GetState(typeof(KongJumpHoldState));

        AddTransition(idleHold, KongStateMachineHelper.ShouldIdle(controller));
        AddTransition(dropping, new FunctionPredicate(() => !controller.Hold && controller.VerticalValue < -0.5));
        AddTransition(throwing, new FunctionPredicate(() => !controller.Hold && controller.VerticalValue > -0.5));
        AddTransition(jumpHold, KongStateMachineHelper.ShouldJump(controller));
    }

    #endregion

    #region State Events

    public override void OnStateStart()
    {
        animator.Play(KongController.Animations.WalkHold);
    }

    public override void OnStateFixedUpdate()
    {
        controller.PerformVelocityHorizontalMovement(KongController.Instance.MovementSettings.RunSpeed);
    }

    #endregion
}