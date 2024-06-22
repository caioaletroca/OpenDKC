using UnityEngine;

public class KongAttackToStandState : BaseState<KongController>
{
    #region Constructor

    public KongAttackToStandState(KongController controller, Animator animator) : base(controller, animator) { }

    #endregion

    #region Public Methods

    public override void RegisterTransitions(BaseStateMachine<KongController> stateMachine)
    {
        var idle = stateMachine.GetStateByType(typeof(KongIdleState));
        
        AddTransition(idle, new CompositePredicate(
            new IPredicate[] {
                new AnimationPredicate(animator, KongController.Animations.AttackToStand, AnimationPredicate.Timing.End),
                new FunctionPredicate(() => controller.HorizontalValue < 0.001 && !controller.Run)
            }
        ));
    }

    #endregion

    #region State Events

    public override void OnStateStart()
    {
        animator.Play(KongController.Animations.AttackToStand);
    }

    #endregion
}