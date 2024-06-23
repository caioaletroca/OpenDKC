using UnityEngine;

public class KongRunState : BaseState<KongController>
{
    #region Constructor

    public KongRunState(KongController controller, Animator animator) : base(controller, animator) { }

    #endregion

    #region Public Methods

    public override void RegisterTransitions(BaseStateMachine<KongController> stateMachine)
    {
        var idle = stateMachine.GetState(typeof(KongIdleState));
        var walk = stateMachine.GetState(typeof(KongWalkState));
        var airborn = stateMachine.GetState(typeof(KongAirbornStateMachine));
        var crouch = stateMachine.GetState(typeof(KongCrouchStateMachine));

        AddTransition(walk, new FunctionPredicate(() => controller.HorizontalValue > 0.001 && !controller.Run));
        AddTransition(idle, new FunctionPredicate(() => controller.HorizontalValue < 0.001));
        AddTransition(airborn, new FunctionPredicate(() => controller.Jump));
        AddTransition(airborn, new FunctionPredicate(() => !controller.Grounded));
        AddTransition(crouch, new FunctionPredicate(() => controller.VerticalValue < -0.5));
    }

    #endregion

    #region State Methods

    public override void OnStateStart()
    {
        controller.EnableGravity();

        animator.Play(KongController.Animations.Run);
    }

    public override void OnStateUpdate()
    {
        controller.PerformVelocityHorizontalMovement(KongController.Instance.MovementSettings.RunSpeed);
    }

    #endregion
}