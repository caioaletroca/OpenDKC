using UnityEngine;

public class KongHurtAirState : BaseState<KongController>
{
    #region Constructor

    public KongHurtAirState(KongController controller, Animator animator) : base(controller, animator) { }

    #endregion

    #region Public Methods

    public override void RegisterTransitions(BaseStateMachine<KongController> stateMachine)
    {
        var landHurt = stateMachine.GetState(typeof(KongHurtLandState));

        AddTransition(landHurt, new FunctionPredicate(() => controller.Grounded));
    }

    #endregion

    #region State Events

    public override void OnStateStart()
    {
        animator.Play(KongController.Animations.FallHurt);
    }

    #endregion
}