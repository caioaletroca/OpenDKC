using UnityEngine;

public class KongRopeIdleState : BaseState<KongController>
{
    #region Constructor

    public KongRopeIdleState(KongController controller, Animator animator) : base(controller, animator) { }

    #endregion

    #region Public Methods

    public override void RegisterTransitions(BaseStateMachine<KongController> stateMachine)
    {
        var turn = stateMachine.GetState(typeof(KongRopeTurnState));
        var vertical = stateMachine.GetState(typeof(KongRopeVerticalState));

        AddTransition(turn, new FunctionPredicate(() => controller.HorizontalValue > 0.001));
        AddTransition(vertical, new FunctionPredicate(() => controller.VerticalValue != 0));
    }

    #endregion

    #region State Events

    public override void OnStateStart()
    {
        controller.PerformVelocityHorizontalMovement(0);
        controller.DisableGravity();
        controller.SetVelocity(Vector2.zero);
        
        animator.Play(KongController.Animations.RopeIdle);
    }

    public override void OnStateFixedUpdate()
    {
        controller.PerformVelocityHorizontalMovement(0);
        controller.PerformVelocityVerticalMovement(0);
    }

    #endregion
}