using UnityEngine;

public class KongRopeTurnState : BaseState<KongController>
{
    #region Constructor

    public KongRopeTurnState(KongController controller, Animator animator) : base(controller, animator) { }

    #endregion

    #region Public Methods

    public override void RegisterTransitions(BaseStateMachine<KongController> stateMachine)
    {
        var idle = stateMachine.GetState(typeof(KongRopeIdleState));
        var vertical = stateMachine.GetState(typeof(KongRopeVerticalState));

        AddTransition(idle, new CompositePredicate(
            new IPredicate[] {
                new AnimationPredicate(animator, KongController.Animations.RopeTurn, AnimationPredicate.Timing.End),
                new FunctionPredicate(() => controller.VerticalValue == 0)
            }
        ));
        AddTransition(vertical, new CompositePredicate(
            new IPredicate[] {
                new AnimationPredicate(animator, KongController.Animations.RopeTurn, AnimationPredicate.Timing.End),
                new FunctionPredicate(() => controller.VerticalValue != 0)
            }
        ));
    }

    #endregion

    #region State Events

    public override void OnStateStart()
    {
        animator.Play(KongController.Animations.RopeTurn);
    }

    public override void OnStateFixedUpdate()
    {
        controller.PerformVelocityHorizontalMovement(0);
        controller.PerformVelocityVerticalMovement(controller.RopeSettings.VerticalSpeed);
    }

    #endregion
}