using UnityEngine;

public class KongIdleState : BaseState<KongController> {
    #region Constructor

    public KongIdleState(KongController controller, Animator animator) : base(controller, animator) { }

    #endregion

    #region Public Methods

    public override void RegisterTransitions(BaseStateMachine<KongController> stateMachine)
    {
        var walk = stateMachine.GetState(typeof(KongWalkState));
        var run = stateMachine.GetState(typeof(KongRunState));
        var airborn = stateMachine.GetState(typeof(KongAirbornStateMachine));
        var attack = stateMachine.GetState(typeof(KongAttackState));
        var crouch = stateMachine.GetState(typeof(KongCrouchStateMachine));

        var air = ((KongAirbornStateMachine)airborn).GetState(typeof(KongAirbornAirState));

        AddTransition(walk, KongStateMachineHelper.ShouldWalk(controller));
        AddTransition(run, KongStateMachineHelper.ShouldRun(controller));
        AddTransition(airborn, KongStateMachineHelper.ShouldJump(controller));
        AddTransition(air, new FunctionPredicate(() => !controller.Grounded));
        AddTransition(attack, new FunctionPredicate(() => controller.Attack));
        AddTransition(crouch, KongStateMachineHelper.ShouldCrouch(controller));
    }

    #endregion

    #region State Events

    public override void OnStateStart()
    {
        animator.Play(KongController.Animations.Idle);
    }

    public override void OnStateFixedUpdate()
    {
        controller.EnableGravity();
        controller.PerformVelocityHorizontalMovement(0);
    }

    #endregion
}