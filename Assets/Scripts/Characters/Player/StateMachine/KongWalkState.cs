using UnityEngine;

public class KongWalkState : BaseState<KongController> {
    #region Constructor

    public KongWalkState(KongController controller, Animator animator) : base(controller, animator) { }

    #endregion

    #region Public Methods

    public override void RegisterTransitions(BaseStateMachine<KongController> stateMachine)
    {
        var idle = stateMachine.GetState(typeof(KongIdleState));
        var airborn = stateMachine.GetState(typeof(KongAirbornStateMachine));
        var attack = stateMachine.GetState(typeof(KongAttackState));
        var crouch = stateMachine.GetState(typeof(KongCrouchStateMachine));
        var picking = stateMachine.GetState(typeof(KongPickingState));

        var air = ((KongAirbornStateMachine)airborn).GetState(typeof(KongAirbornAirState));

        AddTransition(idle, KongStateMachineHelper.ShouldIdle(controller));
        AddTransition(airborn, KongStateMachineHelper.ShouldJump(controller));
        AddTransition(air, new FunctionPredicate(() => !controller.Grounded));
        AddTransition(picking, new FunctionPredicate(() => controller.Hold));
        AddTransition(attack, new FunctionPredicate(() => controller.Attack));
        AddTransition(crouch, KongStateMachineHelper.ShouldCrouch(controller));
    }

    #endregion

    #region State Methods

    public override void OnStateStart()
    {
        controller.EnableGravity();

        animator.Play(KongController.Animations.Walk);
    }

    public override void OnStateFixedUpdate() {
        controller.PerformVelocityHorizontalMovement(KongController.Instance.MovementSettings.WalkSpeed);
    }

    #endregion
}