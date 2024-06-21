using UnityEngine;

public class KongIdleState : BaseState<KongController> {
    #region Constructor

    public KongIdleState(KongController controller, Animator animator) : base(controller, animator) { }

    #endregion

    #region Public Methods

    public override void RegisterTransitions(BaseStateMachine<KongController> stateMachine)
    {
        var walk = stateMachine.GetStateByType(typeof(KongWalkState));
        var airborn = stateMachine.GetStateByType(typeof(KongAirbornMachine));

        AddTransition(walk, new FunctionPredicate(() => controller.HorizontalValue > 0.001));
        AddTransition(airborn, new FunctionPredicate(() => controller.Jump));
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