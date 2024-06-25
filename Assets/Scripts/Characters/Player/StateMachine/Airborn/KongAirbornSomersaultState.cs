using UnityEngine;

public class KongAirbornSomersaultState : BaseState<KongController>
{
    #region Constructor

    public KongAirbornSomersaultState(KongController controller, Animator animator) : base(controller, animator) { }

    #endregion

    #region Public Methods

    public override void RegisterTransitions(BaseStateMachine<KongController> stateMachine)
    {
        
    }

    #endregion

    #region State Events

    public override void OnStateStart()
    {
        animator.Play(KongController.Animations.Somersault);
    }

    public override void OnStateFixedUpdate()
    {
        controller.PerformForceHorizontalMovement(controller.MovementSettings.ForceMagnitude);
    }

    public override void OnStateExit()
    {
        controller.BounceDamager.enabled = false;
    }

    #endregion
}