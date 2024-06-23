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

        AddTransition(walk, new FunctionPredicate(() => controller.HorizontalValue > 0.001 && !controller.Run));
        AddTransition(run, new FunctionPredicate(() => controller.HorizontalValue > 0.001 && controller.Run));
        AddTransition(airborn, new FunctionPredicate(() => controller.Jump));
        AddTransition(attack, new FunctionPredicate(() => controller.Attack));
        AddTransition(crouch, new FunctionPredicate(() => controller.VerticalValue < -0.5));
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