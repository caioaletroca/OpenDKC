using UnityEngine;

public class KongAirbornRiseState : BaseState<KongController>
{
    #region Constructor

    public KongAirbornRiseState(KongController controller, Animator animator) : base(controller, animator) { }

    #endregion

    public override void RegisterTransitions(BaseStateMachine<KongController> stateMachine)
    {
        var air = stateMachine.GetStateByType(typeof(KongAirbornAirState));

        AddTransition(air, new FunctionPredicate(() => {
            return 
                animator.GetCurrentAnimatorStateInfo(0).IsName("diddy_rise") &&
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f &&
                controller.VerticalSpeed < 0;
        }));
    }

    #region State Events

    public override void OnStateStart()
    {
        controller.PerformJump();

        animator.Play(KongController.Animations.Rise);
    }

    public override void OnStateFixedUpdate()
    {
        controller.PerformVelocityHorizontalMovement(KongController.Instance.MovementSettings.WalkSpeed);
    }

    #endregion
}