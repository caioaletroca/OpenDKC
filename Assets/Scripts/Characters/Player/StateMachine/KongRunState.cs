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
        var picking = stateMachine.GetState(typeof(KongPickingState));
        var crouch = stateMachine.GetState(typeof(KongCrouchStateMachine));

        var air = ((KongAirbornStateMachine)airborn).GetState(typeof(KongAirbornAirState));

        AddTransition(walk, new FunctionPredicate(() => controller.HorizontalValue > 0.001 && !controller.Run));
        AddTransition(idle, KongStateMachineHelper.ShouldIdle(controller));
        AddTransition(airborn, KongStateMachineHelper.ShouldJump(controller));
        AddTransition(air, new FunctionPredicate(() => !controller.Grounded));
        AddTransition(picking, new FunctionPredicate(() => controller.Hold));
        AddTransition(crouch, KongStateMachineHelper.ShouldCrouch(controller));
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