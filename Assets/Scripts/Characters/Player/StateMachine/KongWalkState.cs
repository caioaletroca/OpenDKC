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

        AddTransition(idle, new FunctionPredicate(() => controller.HorizontalValue < 0.001));
        AddTransition(airborn, new FunctionPredicate(() => controller.Jump));
        AddTransition(air, new FunctionPredicate(() => !controller.Grounded));
        AddTransition(picking, new FunctionPredicate(() => controller.Hold));
        AddTransition(attack, new FunctionPredicate(() => controller.Attack));
        AddTransition(crouch, new FunctionPredicate(() => controller.VerticalValue < -0.5));
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