using UnityEngine;

public class KongRopeState : BaseState<KongController>
{
    #region Constructor

    public KongRopeState(KongController controller, Animator animator) : base(controller, animator) { }

    #endregion

    #region State Events

    public override void OnStateStart()
    {
        controller.PerformVelocityHorizontalMovement(0);
        controller.DisableGravity();
        controller.SetVelocity(Vector2.zero);

        animator.Play(KongController.Animations.RopeIdle);
    }

    public override void OnStateFixedUpdate()
    {
        controller.PerformVelocityHorizontalMovement(0);
    }

    #endregion
}