using UnityEngine;

public class KongRunState : BaseState<KongController>
{
    #region Constructor

    public KongRunState(KongController controller, Animator animator) : base(controller, animator) { }

    #endregion

    #region Public Methods

    public override void RegisterTransitions(BaseStateMachine<KongController> stateMachine)
    {
        var idle = stateMachine.GetStateByType(typeof(KongIdleState));
        var walk = stateMachine.GetStateByType(typeof(KongWalkState));
        var airborn = stateMachine.GetStateByType(typeof(KongAirbornMachine));

        AddTransition(walk, new FunctionPredicate(() => controller.HorizontalValue > 0.001 && !controller.Run));
        AddTransition(idle, new FunctionPredicate(() => controller.HorizontalValue < 0.001));
        AddTransition(airborn, new FunctionPredicate(() => controller.Jump));
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