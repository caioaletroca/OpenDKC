using UnityEngine;

public class KongStandToCrouchState : BaseState<KongController>
{
    #region Constructor

    public KongStandToCrouchState(KongController controller, Animator animator) : base(controller, animator) { }

    #endregion

    #region Public Methods

    public override void RegisterTransitions(BaseStateMachine<KongController> stateMachine)
    {
        var crouch = stateMachine.GetState(typeof(KongCrouchState));
        var crouchToStand = stateMachine.GetState(typeof(KongCrouchToStandState));

        AddTransition(crouch, new CompositePredicate(
            new IPredicate[] {
                new AnimationPredicate(animator, KongController.Animations.StandToCrouch, AnimationPredicate.Timing.End),
                KongStateMachineHelper.ShouldCrouch(controller)
            }
        ));
        AddTransition(crouchToStand, new CompositePredicate(
            new IPredicate[] {
                new AnimationPredicate(animator, KongController.Animations.StandToCrouch, AnimationPredicate.Timing.End),
                new FunctionPredicate(() => controller.VerticalValue > -0.5)
            }
        ));
    }

    #endregion

    #region State Events

    public override void OnStateStart()
    {
        animator.Play(KongController.Animations.StandToCrouch);
    }

    public override void OnStateUpdate()
    {
        controller.PerformVelocityHorizontalMovement(KongController.Instance.MovementSettings.CrouchSpeed);
    }

    #endregion
}