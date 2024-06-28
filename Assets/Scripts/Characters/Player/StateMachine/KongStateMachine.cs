using UnityEngine;

public class KongStateMachine : BaseStateMachine<KongController> {
    #region Constructor

    public KongStateMachine(KongController controller, Animator animator) : base(controller, animator) {
        AddState(new KongIdleState(controller, animator));
        AddState(new KongWalkState(controller, animator));
        AddState(new KongRunState(controller, animator));
        AddState(new KongAirbornStateMachine(controller, animator));
        AddState(new KongAttackState(controller, animator));
        AddState(new KongAttackToStandState(controller, animator));
        AddState(new KongHookStateMachine(controller, animator));
        AddState(new KongCrouchStateMachine(controller, animator));
        AddState(new KongInsideBarrelState(controller, animator));
        AddState(new KongBlastState(controller, animator));
        AddState(new KongPickingState(controller, animator));
        AddState(new KongIdleHoldState(controller, animator));
        AddState(new KongWalkHoldState(controller, animator));
        AddState(new KongDroppingState(controller, animator));
        AddState(new KongThrowState(controller, animator));

        AddAnyState(new KongHurtStateMachine(controller, animator));

        RegisterStateTransitions();

        SetState(typeof(KongIdleState));
    }

    #endregion
}