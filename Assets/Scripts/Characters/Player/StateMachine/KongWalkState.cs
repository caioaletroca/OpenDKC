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

        AddTransition(idle, new FunctionPredicate(() => controller.HorizontalValue < 0.001));
        AddTransition(airborn, new FunctionPredicate(() => controller.Jump));
        AddTransition(airborn, new FunctionPredicate(() => !controller.Grounded));
        AddTransition(attack, new FunctionPredicate(() => controller.Attack));
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