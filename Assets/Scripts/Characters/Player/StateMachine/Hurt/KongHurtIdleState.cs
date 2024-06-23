using UnityEngine;

public class KongHurtIdleState : BaseState<KongController>
{
    #region Constructor

    public KongHurtIdleState(KongController controller, Animator animator) : base(controller, animator) { }

    #endregion

    #region State Events

    public override void OnStateStart()
    {
        controller.SetVelocity(Vector2.zero);
        
        animator.Play(KongController.Animations.IdleHurt);
    }

    public override void OnStateFixedUpdate()
    {
        controller.SetVelocity(Vector2.zero);
    }

    #endregion
}