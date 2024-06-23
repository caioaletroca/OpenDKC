using UnityEngine;

public class KongHurtRiseState : BaseState<KongController>
{
    #region Constructor

    public KongHurtRiseState(KongController controller, Animator animator) : base(controller, animator) { }

    #endregion

    #region Public Methods

    public override void RegisterTransitions(BaseStateMachine<KongController> stateMachine)
    {
        var airHurt = stateMachine.GetState(typeof(KongHurtAirState));

        AddTransition(airHurt, new CompositePredicate(
            new IPredicate[] {
                new AnimationPredicate(animator, KongController.Animations.AirHurt, AnimationPredicate.Timing.End),
                new FunctionPredicate(() => controller.VerticalSpeed < 0)
            }
        ));
    }

    #endregion

    #region State Events

    public override void OnStateStart()
    {
        controller.PerformVelocityHorizontalMovement(0);
        controller.SetVelocity(Vector2.zero);
        controller.PerformDeathJump();
        
        animator.Play(KongController.Animations.AirHurt);
    }

    #endregion
}