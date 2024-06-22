using UnityEngine;

public class KongHookHookingState : BaseState<KongController>
{
    #region Constructor

    public KongHookHookingState(KongController controller, Animator animator) : base(controller, animator) { }

    #endregion

    #region Public Methods

    public override void RegisterTransitions(BaseStateMachine<KongController> stateMachine)
    {
        var hook = stateMachine.GetState(typeof(KongHookHookState));

        AddTransition(hook, new AnimationPredicate(animator, KongController.Animations.Hooking, AnimationPredicate.Timing.End));
    }

    #endregion

    #region State Events

    public override void OnStateStart()
    {
        controller.PerformVelocityHorizontalMovement(0);
        controller.DisableGravity();
        controller.SetVelocity(Vector2.zero);
        controller.SetLocalPosition(controller.HookSettings.SnapOffset);

        animator.Play(KongController.Animations.Hooking);
    }

    #endregion
}