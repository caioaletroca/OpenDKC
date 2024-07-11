using UnityEngine;

public class KongRopeHorizontalState : BaseState<KongController>
{
    #region Constructor

    public KongRopeHorizontalState(KongController controller, Animator animator) : base(controller, animator) { }

    #endregion

    #region Public Methods

    public override void RegisterTransitions(BaseStateMachine<KongController> stateMachine)
    {
        
    }

    #endregion

    #region State Events

    #endregion
}