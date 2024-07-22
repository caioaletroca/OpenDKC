using UnityEngine;

public class KongAttackState : BaseState<KongController>
{
    #region Private Properties

    /// <summary>
    /// Flag to control if we should perform a attack from idle state
    /// </summary>
    bool IdleAttack = false;

    #endregion

    #region Constructor

    public KongAttackState(KongController controller, Animator animator) : base(controller, animator) { }

    #endregion

    #region Public Methods

    public override void RegisterTransitions(BaseStateMachine<KongController> stateMachine)
    {
        var run = stateMachine.GetState(typeof(KongRunState));
        var airborn = stateMachine.GetState(typeof(KongAirbornStateMachine));
        var picking = stateMachine.GetState(typeof(KongPickingState));
        var attackToStand = stateMachine.GetState(typeof(KongAttackToStandState));

        AddTransition(run, new CompositePredicate(
            new IPredicate[] {
                new AnimationPredicate(animator, KongController.Animations.Attack, AnimationPredicate.Timing.End),
                KongStateMachineHelper.ShouldRun(controller)
            }
        ));
        AddTransition(airborn, KongStateMachineHelper.ShouldJump(controller));
        AddTransition(picking, new FunctionPredicate(() => controller.Hold));
        AddTransition(attackToStand, new AnimationPredicate(animator, KongController.Animations.Attack, AnimationPredicate.Timing.End));
    }

    #endregion

    #region State Events

    public override void OnStateStart()
    {
        controller.EnableGravity();
        controller.Damageable.EnableInvulnerability(true);
        controller.AttackDamager.enabled = true;

        if(controller.HorizontalValue < 0.001) {
            IdleAttack = true;

            controller.PerformIdleAttack();
        }

        animator.Play(KongController.Animations.Attack);

        // Register event
        controller.FlipTrigger.AddListener(OnFlip);
    }

    public override void OnStateFixedUpdate()
    {       
        if(IdleAttack) {
            return;
        }

        controller.PerformVelocityHorizontalMovement(KongController.Instance.MovementSettings.RunSpeed);
    }

    public override void OnStateExit()
    {
        controller.Damageable.DisableInvulnerability();
        controller.AttackDamager.enabled = false;
        IdleAttack = false;

        // Unregister event
        controller.FlipTrigger.RemoveListener(OnFlip);
    }

    #endregion

    #region Private Methods

    private void OnFlip(bool direction) {
        // If the kong has flipped direction before 0.42f frame
        // Skip animation to that position, making it end earlier
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.42f) {
            animator.Play(KongController.Animations.Attack, 0, 0.42f);
        }
    }

    #endregion
}