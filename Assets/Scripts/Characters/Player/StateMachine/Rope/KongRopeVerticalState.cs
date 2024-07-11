using UnityEngine;

public class KongRopeVerticalState : BaseState<KongController>
{
    #region Constructor

    public KongRopeVerticalState(KongController controller, Animator animator) : base(controller, animator) { }

    #endregion

    #region Public Methods

    public override void RegisterTransitions(BaseStateMachine<KongController> stateMachine)
    {
        
    }

    #endregion

    #region State Events

    #endregion
}