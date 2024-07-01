using UnityEngine;

public class KongThrowState : BaseState<KongController>
{   
    #region Constructor

    public KongThrowState(KongController controller, Animator animator) : base(controller, animator) { }

    #endregion

    #region Public Methods

    public override void RegisterTransitions(BaseStateMachine<KongController> stateMachine)
    {
        var idle = stateMachine.GetState(typeof(KongIdleState));
        var walk = stateMachine.GetState(typeof(KongWalkState));

        AddTransition(walk, new CompositePredicate(
            new IPredicate[] {
                new AnimationPredicate(animator, KongController.Animations.Throw, AnimationPredicate.Timing.End),
                new FunctionPredicate(() => controller.HorizontalValue > 0.001)
            }
        ));
        AddTransition(idle, new CompositePredicate(
            new IPredicate[] {
                new AnimationPredicate(animator, KongController.Animations.Throw, AnimationPredicate.Timing.End),
                new FunctionPredicate(() => controller.HorizontalValue < 0.001)
            }
        ));
    }

    #endregion

    #region State Events

    public override void OnStateStart()
    {
        controller.PerformFreezeThrow();

        animator.Play(KongController.Animations.Throw);
    }

    public override void OnStateFixedUpdate()
    {
        controller.PerformVelocityHorizontalMovement(0);
    }

    public override void OnStateExit()
    {
        controller.PerformUnfreezeThrow();
    }

    #endregion
}