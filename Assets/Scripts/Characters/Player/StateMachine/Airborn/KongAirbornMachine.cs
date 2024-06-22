using UnityEngine;

public class KongAirbornMachine : BaseStateMachine<KongController>
{
    #region Constructor

    public KongAirbornMachine(KongController controller, Animator animator) : base(controller, animator) {
        AddState(new KongAirbornRiseState(controller, animator));
        AddState(new KongAirbornAirState(controller, animator));
        AddState(new KongAirbornLandState(controller, animator));

        RegisterStateTransitions();
    }

    #endregion

    #region Public Methods

    public override void RegisterTransitions(BaseStateMachine<KongController> stateMachine)
    {
        var idle = stateMachine.GetStateByType(typeof(KongIdleState));

        AddTransition(idle, new AnimationPredicate(animator, KongController.Animations.Land, AnimationPredicate.Timing.End));
    }

    #endregion

    #region State Events

    public override void OnStateStart()
    {
        if(controller.Jump) {
            SetState(typeof(KongAirbornRiseState));
            return;
        }

        if(!controller.Grounded) {
            SetState(typeof(KongAirbornAirState));
            return;
        }
    }

    #endregion
}