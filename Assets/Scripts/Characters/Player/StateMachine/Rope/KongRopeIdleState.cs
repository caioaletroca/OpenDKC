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

        AddTransition(turn, new FunctionPredicate(() => 
            controller.FacingRight && controller.HorizontalValue > 0 ||
            !controller.FacingRight && controller.HorizontalValue < 0
        ));
        AddTransition(vertical, new FunctionPredicate(() => controller.VerticalValue != 0));
    }

    #endregion

    #region State Events

    public override void OnStateStart()
    {
        controller.PerformRopeGrap();
        
        animator.Play(KongController.Animations.RopeIdle);
    }

    public override void OnStateFixedUpdate()
    {
        // Debug.Log(controller.RopeController.CheckHorizontalRightMovement());

        controller.PerformVelocityHorizontalMovement(0);
        controller.PerformVelocityVerticalMovement(0);
    }

    #endregion
}