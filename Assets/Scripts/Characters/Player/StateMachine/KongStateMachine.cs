using UnityEngine;

public class KongStateMachine : BaseStateMachine<KongController> {
    #region Constructor

    public KongStateMachine(KongController controller, Animator animator) : base(controller, animator) {
        AddState(new KongIdleState(controller, animator));
        AddState(new KongWalkState(controller, animator));
        AddState(new KongAirbornMachine(controller, animator));

        RegisterStateTransitions();

        SetState(typeof(KongIdleState));
    }

    #endregion
}