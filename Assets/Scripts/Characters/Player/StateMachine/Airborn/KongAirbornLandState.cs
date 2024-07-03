using UnityEngine;

public class KongAirbornLandState : BaseState<KongController>
{
    #region Constructor

    public KongAirbornLandState(KongController controller, Animator animator) : base(controller, animator) { }

    #endregion

    #region Public Methods

    public override void RegisterTransitions(BaseStateMachine<KongController> stateMachine)
    {
        var airToSomersault = stateMachine.GetState(typeof(KongAirbornAirToSomersaultState));

        AddTransition(airToSomersault, new FunctionPredicate(() => controller.Somersault));
    }

    #endregion

    #region State Events

    public override void OnStateStart()
    {
        animator.Play(KongController.Animations.Land);
    }

    public override void OnStateFixedUpdate()
    {
        controller.PerformVelocityHorizontalMovement(controller.Run ? KongController.Instance.MovementSettings.RunSpeed : KongController.Instance.MovementSettings.WalkSpeed);
    }

    public override void OnStateExit()
    {
        controller.BounceDamager.enabled = false;
    }

    #endregion
}