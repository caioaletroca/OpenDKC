using UnityEngine;

public class KongAirbornAirState : BaseState<KongController>
{
    #region Constructor

    public KongAirbornAirState(KongController controller, Animator animator) : base(controller, animator) { }

    #endregion

    #region Public Methods

    public override void RegisterTransitions(BaseStateMachine<KongController> stateMachine)
    {
        var land = stateMachine.GetState(typeof(KongAirbornLandState));
        var airToSomersault = stateMachine.GetState(typeof(KongAirbornAirToSomersaultState));

        AddTransition(land, new FunctionPredicate(() => controller.GroundDistance < 1));
        AddTransition(airToSomersault, new FunctionPredicate(() => controller.Somersault));
    }

    #endregion

    #region State Events

    public override void OnStateStart()
    {
        controller.BounceDamager.enabled = true;

        animator.Play(KongController.Animations.Air);
    }

    public override void OnStateFixedUpdate()
    {
        controller.PerformVelocityHorizontalMovement(controller.Run ? KongController.Instance.MovementSettings.RunSpeed : KongController.Instance.MovementSettings.WalkSpeed);
    }

    #endregion
}