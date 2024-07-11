using UnityEngine;

public class KongRopeStateMachine : BaseStateMachine<KongController>
{
    #region Constructor

    public KongRopeStateMachine(KongController controller, Animator animator) : base(controller, animator) {
        AddState(new KongRopeIdleState(controller, animator));
        AddState(new KongRopeVerticalState(controller, animator));
        AddState(new KongRopeHorizontalState(controller, animator));

        RegisterStateTransitions();

        SetCurrent(typeof(KongRopeIdleState));
    }

    #endregion

    #region Public Methods

    public override void RegisterTransitions(BaseStateMachine<KongController> stateMachine)
    {
        
    }

    #endregion

    #region State Events

    #endregion
}