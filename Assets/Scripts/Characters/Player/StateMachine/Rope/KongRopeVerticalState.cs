using UnityEngine;

public class KongRopeVerticalState : BaseState<KongController>
{
    #region Constructor

    public KongRopeVerticalState(KongController controller, Animator animator) : base(controller, animator) { }

    #endregion

    #region Public Methods

    public override void RegisterTransitions(BaseStateMachine<KongController> stateMachine)
    {
        var idle = stateMachine.GetState(typeof(KongRopeIdleState));

        AddTransition(idle, new FunctionPredicate(() => controller.VerticalValue == 0));
    }

    #endregion

    #region State Events

    public override void OnStateStart()
    {
        if(controller.VerticalValue > 0)
            animator.Play(KongController.Animations.RopeVerticalUp);
        else
            animator.Play(KongController.Animations.RopeVerticalDown);
    }

    public override void OnStateFixedUpdate()
    {
        controller.PerformVelocityHorizontalMovement(0);
        controller.PerformRopeVerticalMovement();
        // controller.PerformVelocityVerticalMovement(controller.RopeSettings.VerticalSpeed);
    }

    #endregion
}