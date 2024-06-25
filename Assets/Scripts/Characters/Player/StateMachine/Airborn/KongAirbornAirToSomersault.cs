using UnityEngine;

public class KongAirbornAirToSomersaultState : BaseState<KongController>
{
    #region Constructor

    public KongAirbornAirToSomersaultState(KongController controller, Animator animator) : base(controller, animator) { }

    #endregion

    #region Public Methods

    public override void RegisterTransitions(BaseStateMachine<KongController> stateMachine)
    {
        var somersault = stateMachine.GetState(typeof(KongAirbornSomersaultState));

        AddTransition(somersault, new AnimationPredicate(animator, KongController.Animations.AirToSomersault, AnimationPredicate.Timing.End));
    }

    #endregion

    #region State Events

    public override void OnStateStart()
    {
        controller.PerformJump();
        
        animator.Play(KongController.Animations.AirToSomersault);
    }

    #endregion
}