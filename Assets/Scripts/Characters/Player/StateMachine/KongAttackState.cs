using UnityEngine;

public class KongAttackState : BaseState<KongController>
{
    #region Private Properties

    /// <summary>
    /// Flag to control if we should perform a attack from idle state
    /// </summary>
    bool idleAttack = false;

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
                new FunctionPredicate(() => controller.Run)
            }
        ));
        AddTransition(airborn, new FunctionPredicate(() => controller.Jump));
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
            idleAttack = true;

            controller.PerformIdleAttack();
        }

        animator.Play(KongController.Animations.Attack);
    }

    public override void OnStateUpdate()
    {
        if(idleAttack) {
            return;
        }

        controller.PerformVelocityHorizontalMovement(KongController.Instance.MovementSettings.RunSpeed);
    }

    public override void OnStateExit()
    {
        controller.Damageable.DisableInvulnerability();
        controller.AttackDamager.enabled = false;
        idleAttack = false;
    }

    #endregion
}