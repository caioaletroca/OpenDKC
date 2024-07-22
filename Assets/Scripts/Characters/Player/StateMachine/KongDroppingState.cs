using UnityEngine;

public class KongDroppingState : BaseState<KongController>
{
    #region Constructor

    public KongDroppingState(KongController controller, Animator animator) : base(controller, animator) { }

    #endregion

    #region Public Methods

    public override void RegisterTransitions(BaseStateMachine<KongController> stateMachine)
    {
        var idle = stateMachine.GetState(typeof(KongIdleState));
        var walk = stateMachine.GetState(typeof(KongWalkState));
        var run = stateMachine.GetState(typeof(KongRunState));

        AddTransition(idle, new CompositePredicate(
            new IPredicate[] {
                new AnimationPredicate(animator, KongController.Animations.Dropping, AnimationPredicate.Timing.End),
                KongStateMachineHelper.ShouldIdle(controller),
            }
        ));
        AddTransition(walk, new CompositePredicate(
            new IPredicate[] {
                new AnimationPredicate(animator, KongController.Animations.Dropping, AnimationPredicate.Timing.End),
                new FunctionPredicate(() => !controller.Run && controller.HorizontalValue > 0.001),
            }
        ));
        AddTransition(run, new CompositePredicate(
            new IPredicate[] {
                new AnimationPredicate(animator, KongController.Animations.Dropping, AnimationPredicate.Timing.End),
                new FunctionPredicate(() => controller.Run && controller.HorizontalValue > 0.001),
            }
        ));
    }

    #endregion

    #region State Events

    public override void OnStateStart()
    {
        // Breafly slows downs player
        controller.PerformVelocityHorizontalMovement(0);

        animator.Play(KongController.Animations.Dropping);
    }

    public override void OnStateFixedUpdate()
    {
        controller.PerformVelocityHorizontalMovement(0);
    }

    public override void OnStateExit()
    {
        controller.PerformItemDrop();
    }

    #endregion
}