using UnityEngine;

public class KongIdleHoldState : BaseState<KongController>
{
    #region Constructor

    public KongIdleHoldState(KongController controller, Animator animator) : base(controller, animator) { }

    #endregion

    #region Public Methods

    public override void RegisterTransitions(BaseStateMachine<KongController> stateMachine)
    {
        var walkHold = stateMachine.GetState(typeof(KongWalkHoldState));
        var dropping = stateMachine.GetState(typeof(KongDroppingState));
        var throwing = stateMachine.GetState(typeof(KongThrowState));

        AddTransition(walkHold, new FunctionPredicate(() => controller.HorizontalValue > 0.001));
        AddTransition(dropping, new FunctionPredicate(() => !controller.Hold && controller.VerticalValue < -0.5));
        AddTransition(throwing, new FunctionPredicate(() => !controller.Hold && controller.VerticalValue > -0.5));
    }

    #endregion

    #region State Events

    public override void OnStateStart()
    {
        animator.Play(KongController.Animations.IdleHold);
    }

    public override void OnStateFixedUpdate()
    {
        controller.EnableGravity();
        controller.PerformVelocityHorizontalMovement(0);
    }

    #endregion
}