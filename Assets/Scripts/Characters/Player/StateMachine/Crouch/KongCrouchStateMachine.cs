using UnityEngine;

public class KongCrouchStateMachine : BaseStateMachine<KongController>
{
    #region Constructor

    public KongCrouchStateMachine(KongController controller, Animator animator) : base(controller, animator)
    {
        AddState(new KongStandToCrouchState(controller, animator));
        AddState(new KongCrouchState(controller, animator));
        AddState(new KongCrouchToStandState(controller, animator));

        RegisterStateTransitions();
    }

    #endregion

    #region Public Methods

    public override void RegisterTransitions(BaseStateMachine<KongController> stateMachine)
    {
        var idle = stateMachine.GetState(typeof(KongIdleState));
        var walk = stateMachine.GetState(typeof(KongWalkState));
        var run = stateMachine.GetState(typeof(KongRunState));

        AddTransition(idle, new CompositePredicate(
            new IPredicate[] {
                new AnimationPredicate(animator, KongController.Animations.CrouchToStand, AnimationPredicate.Timing.End),
                new FunctionPredicate(() => controller.VerticalValue > -0.5 && controller.HorizontalValue < 0.001),
            }
        ));
        AddTransition(walk, new CompositePredicate(
            new IPredicate[] {
                new AnimationPredicate(animator, KongController.Animations.CrouchToStand, AnimationPredicate.Timing.End),
                new FunctionPredicate(() => controller.VerticalValue > -0.5 && !controller.Run && controller.HorizontalValue > 0.001),
            }
        ));
        AddTransition(run, new CompositePredicate(
            new IPredicate[] {
                new AnimationPredicate(animator, KongController.Animations.CrouchToStand, AnimationPredicate.Timing.End),
                new FunctionPredicate(() => controller.VerticalValue > -0.5 && controller.Run && controller.HorizontalValue > 0.001),
            }
        ));

        SetCurrent(typeof(KongStandToCrouchState));
    }

    #endregion
}