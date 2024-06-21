using UnityEngine;

public class KongWalkState : BaseState<KongController> {
    #region Constructor

    public KongWalkState(KongController controller, Animator animator) : base(controller, animator) { }

    #endregion

    #region Public Methods

    public override void RegisterTransitions(BaseStateMachine<KongController> stateMachine)
    {
        var idle = stateMachine.GetStateByType(typeof(KongIdleState));
        var airborn = stateMachine.GetStateByType(typeof(KongAirbornMachine));

        AddTransition(idle, new FunctionPredicate(() => controller.HorizontalValue < 0.001));
        AddTransition(airborn, new FunctionPredicate(() => controller.Jump));
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