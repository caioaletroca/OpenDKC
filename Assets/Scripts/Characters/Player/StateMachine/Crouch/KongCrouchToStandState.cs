using UnityEngine;

public class KongCrouchToStandState : BaseState<KongController>
{
    #region Constructor

    public KongCrouchToStandState(KongController controller, Animator animator) : base(controller, animator) { }

    #endregion

    #region State Events

    public override void OnStateStart()
    {
        animator.Play(KongController.Animations.CrouchToStand);
    }

    #endregion
}