using UnityEngine;

public class KongInsideBarrelState : BaseState<KongController> {
    #region Constructor

    public KongInsideBarrelState(KongController controller, Animator animator) : base(controller, animator) { }

    #endregion

    #region Public Methods

    public override void RegisterTransitions(BaseStateMachine<KongController> stateMachine)
    {
        var blast = stateMachine.GetState(typeof(KongBlastState));

        AddTransition(blast, new FunctionPredicate(() => controller.Blast));
    }

    #endregion

    #region State Events

    public override void OnStateStart()
    {
        controller.SetVelocity(Vector2.zero);
        controller.DisableGravity();
        controller.SetLocalPosition(Vector2.zero);

        animator.Play(KongController.Animations.Invisible);

        VFXController.Instance.Trigger("CrashNoSoundXF", controller.transform.position, 0, false, null);
    }

    #endregion
}