using UnityEngine;

public class KongAirbornStateMachine : BaseStateMachine<KongController>
{
    #region Constructor

    public KongAirbornStateMachine(KongController controller, Animator animator) : base(controller, animator) {
        AddState(new KongAirbornRiseState(controller, animator));
        AddState(new KongAirbornAirState(controller, animator));
        AddState(new KongAirbornLandState(controller, animator));
        AddState(new KongAirbornAirToSomersaultState(controller, animator));
        AddState(new KongAirbornSomersaultState(controller, animator));

        RegisterStateTransitions();
    }

    #endregion

    #region Public Methods

    public override void RegisterTransitions(BaseStateMachine<KongController> stateMachine)
    {
        var idle = stateMachine.GetState(typeof(KongIdleState));
        var walk = stateMachine.GetState(typeof(KongWalkState));
        var run = stateMachine.GetState(typeof(KongRunState));
        var attack = stateMachine.GetState(typeof(KongAttackState));
        var hook = stateMachine.GetState(typeof(KongHookStateMachine));
        var insideBarrel = stateMachine.GetState(typeof(KongInsideBarrelState));

        var somersault = GetState(typeof(KongAirbornSomersaultState));

        AddTransition(idle, new CompositePredicate(
            new IPredicate[] {
                new CompositePredicate(
                    new IPredicate[] {
                        new AnimationPredicate(animator, KongController.Animations.Land, AnimationPredicate.Timing.End),
                        new FunctionPredicate(() => controller.HorizontalValue < 0.001),
                    }
                ),
                new CompositePredicate(
                    new IPredicate[] {
                        new AnimationPredicate(animator, KongController.Animations.Somersault, AnimationPredicate.Timing.Playing),
                        new FunctionPredicate(() => controller.Grounded && controller.HorizontalValue < 0.001),
                    }
                )
            }, CompositePredicate.Operation.OR
        ));
        AddTransition(walk, new CompositePredicate(
            new IPredicate[] {
                new CompositePredicate(
                    new IPredicate[] {
                        new AnimationPredicate(animator, KongController.Animations.Land, AnimationPredicate.Timing.End),
                        new FunctionPredicate(() => controller.HorizontalValue > 0.001 && !controller.Run)
                    }
                ),
                new CompositePredicate(
                    new IPredicate[] {
                        new AnimationPredicate(animator, KongController.Animations.Somersault, AnimationPredicate.Timing.End),
                        new FunctionPredicate(() => controller.Grounded && controller.HorizontalValue > 0.001 && !controller.Run)
                    }
                )
            }, CompositePredicate.Operation.OR
        ));
        AddTransition(run, new CompositePredicate(
            new IPredicate[] {
                new CompositePredicate(
                    new IPredicate[] {
                        new AnimationPredicate(animator, KongController.Animations.Land, AnimationPredicate.Timing.End),
                        new FunctionPredicate(() => controller.HorizontalValue > 0.001 && controller.Run)
                    }
                ),
                new CompositePredicate(
                    new IPredicate[] {
                        new AnimationPredicate(animator, KongController.Animations.Somersault, AnimationPredicate.Timing.End),
                        new FunctionPredicate(() => controller.Grounded && controller.HorizontalValue > 0.001 && controller.Run)
                    }
                ),
            }, CompositePredicate.Operation.OR
        ));
        AddTransition(attack, new CompositePredicate(
            new IPredicate[] {
                new AnimationPredicate(animator, KongController.Animations.Air, AnimationPredicate.Timing.End),
                new FunctionPredicate(() => controller.Grounded && controller.Attack)
            }
        ));
        AddTransition(hook, new FunctionPredicate(() => controller.Hook));
        AddTransition(insideBarrel, new FunctionPredicate(() => controller.Barrel));

        SetCurrent(typeof(KongAirbornRiseState));
    }

    #endregion
}
