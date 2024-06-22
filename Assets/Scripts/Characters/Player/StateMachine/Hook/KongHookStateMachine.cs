using UnityEngine;

public class KongHookStateMachine : BaseStateMachine<KongController>
{
    #region Constructor

    public KongHookStateMachine(KongController controller, Animator animator) : base(controller, animator)
    {
        AddState(new KongHookHookingState(controller, animator));
        AddState(new KongHookHookState(controller, animator));
        AddState(new KongHookUnhookingState(controller, animator));

        RegisterStateTransitions();

        SetCurrent(typeof(KongHookHookingState));
    }

    #endregion

    #region Public Methods

    public override void RegisterTransitions(BaseStateMachine<KongController> stateMachine)
    {
        var airborn = (KongAirbornStateMachine)stateMachine.GetState(typeof(KongAirbornStateMachine));
        var air = airborn.GetState(typeof(KongAirbornAirState));

        AddTransition(airborn, new FunctionPredicate(() => controller.Jump && controller.VerticalValue > -0.5));
        AddTransition(air, new CompositePredicate(
            new IPredicate[] {
                new AnimationPredicate(animator, KongController.Animations.Rise, AnimationPredicate.Timing.End),
                new FunctionPredicate(() => controller.VerticalSpeed < 0)
            }
        ));
    }

    #endregion

    #region State Events

    public override void OnStateExit()
    {
        base.OnStateExit();

        controller.EnableGravity();
        controller.SetParent(null);
    }

    #endregion
}