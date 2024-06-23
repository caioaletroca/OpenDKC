using UnityEngine;

public class KongCrouchState : BaseState<KongController>
{
    #region Constructor

    public KongCrouchState(KongController controller, Animator animator) : base(controller, animator) { }

    #endregion

    #region Public Methods

    public override void RegisterTransitions(BaseStateMachine<KongController> stateMachine)
    {
        var crouchToStand = stateMachine.GetState(typeof(KongCrouchToStandState));

        AddTransition(crouchToStand, new FunctionPredicate(() => controller.VerticalValue > -0.5));
    }

    #endregion

    #region State Events

    public override void OnStateStart()
    {
        animator.Play(KongController.Animations.Crouch);
    }

    public override void OnStateUpdate()
    {
        controller.PerformVelocityHorizontalMovement(KongController.Instance.MovementSettings.CrouchSpeed);
    }


    #endregion
}