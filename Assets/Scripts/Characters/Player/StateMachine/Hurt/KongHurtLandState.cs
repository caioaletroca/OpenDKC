using UnityEngine;

public class KongHurtLandState : BaseState<KongController>
{
    #region Constructor

    public KongHurtLandState(KongController controller, Animator animator) : base(controller, animator) { }

    #endregion

    #region Public Methods

    public override void RegisterTransitions(BaseStateMachine<KongController> stateMachine)
    {
        var idleHurt = stateMachine.GetState(typeof(KongHurtIdleState));

        AddTransition(idleHurt, new AnimationPredicate(animator, KongController.Animations.LandHurt, AnimationPredicate.Timing.End));
    }

    #endregion

    #region State Events

    public override void OnStateStart()
    {
        controller.SetVelocity(Vector2.zero);

        animator.Play(KongController.Animations.LandHurt);
    }

    public override void OnStateFixedUpdate()
    {
        controller.SetVelocity(Vector2.zero);
    }

    #endregion
}