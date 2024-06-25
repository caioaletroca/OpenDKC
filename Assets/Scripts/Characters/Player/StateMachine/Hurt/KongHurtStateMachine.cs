using UnityEngine;

public class KongHurtStateMachine : BaseStateMachine<KongController>
{
    #region Constructor

    public KongHurtStateMachine(KongController controller, Animator animator) : base(controller, animator)
    {
        AddState(new KongHurtRiseState(controller, animator));
        AddState(new KongHurtAirState(controller, animator));
        AddState(new KongHurtLandState(controller, animator));
        AddState(new KongHurtIdleState(controller, animator));

        RegisterStateTransitions();
    }

    #endregion

    #region Public Methods

    public override void RegisterTransitions(BaseStateMachine<KongController> stateMachine)
    {
        AddAnyTransition(new FunctionPredicate(() => controller.Die));

        SetCurrent(typeof(KongHurtRiseState));
    }

    #endregion
}