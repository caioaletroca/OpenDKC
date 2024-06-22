using UnityEngine;

public class KongHookHookState : BaseState<KongController>
{
    #region Constructor

    public KongHookHookState(KongController controller, Animator animator) : base(controller, animator) { }

    #endregion

    #region Public Methods

    public override void RegisterTransitions(BaseStateMachine<KongController> stateMachine)
    {
        var unhooking = stateMachine.GetState(typeof(KongHookUnhookingState));

        AddTransition(unhooking, new FunctionPredicate(() => controller.Jump && controller.VerticalValue < -0.5));
    }

    #endregion

    #region State Events

    public override void OnStateStart()
    {
        animator.Play(KongController.Animations.Hook);
    }

    public override void OnStateFixedUpdate()
    {
        controller.PerformVelocityHorizontalMovement(0);
        controller.SetLocalPosition(controller.HookSettings.SnapOffset);
    }

    public override void OnStateExit()
    {
        controller.EnableGravity();
        controller.SetParent(null);
    }

    #endregion
}