using UnityEngine;

public class KongPickingState : BaseState<KongController>
{
    #region Constructor

    public KongPickingState(KongController controller, Animator animator) : base(controller, animator) { }

    #endregion

    #region Public Methods

    public override void RegisterTransitions(BaseStateMachine<KongController> stateMachine)
    {
        var idleHold = stateMachine.GetState(typeof(KongIdleHoldState));

        AddTransition(idleHold, new AnimationPredicate(animator, KongController.Animations.Picking, AnimationPredicate.Timing.End));
    }

    #endregion

    #region State Events

    public override void OnStateStart()
    {
        // Breafly slows downs player
        controller.PerformVelocityHorizontalMovement(0);

        animator.Play(KongController.Animations.Picking);
    }

    #endregion
}