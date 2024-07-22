using UnityEngine;

public class KongAttackToStandState : BaseState<KongController>
{
    #region Constructor

    public KongAttackToStandState(KongController controller, Animator animator) : base(controller, animator) { }

    #endregion

    #region Public Methods

    public override void RegisterTransitions(BaseStateMachine<KongController> stateMachine)
    {
        var idle = stateMachine.GetState(typeof(KongIdleState));
        var walk = stateMachine.GetState(typeof(KongWalkState));
        
        AddTransition(idle, new CompositePredicate(
            new IPredicate[] {
                new AnimationPredicate(animator, KongController.Animations.AttackToStand, AnimationPredicate.Timing.End),
                KongStateMachineHelper.ShouldIdle(controller)
            }
        ));
        AddTransition(walk, new CompositePredicate(
            new IPredicate[] {
                new AnimationPredicate(animator, KongController.Animations.AttackToStand, AnimationPredicate.Timing.End),
                KongStateMachineHelper.ShouldWalk(controller)
            }
        ));
    }

    #endregion

    #region State Events

    public override void OnStateStart()
    {   
        animator.Play(KongController.Animations.AttackToStand);
    }

    public override void OnStateFixedUpdate()
    {
        controller.PerformVelocityHorizontalMovement(0);
    }

    #endregion
}