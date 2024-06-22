using UnityEngine;

public class KongAttackState : BaseState<KongController>
{
    #region Constructor

    public KongAttackState(KongController controller, Animator animator) : base(controller, animator) { }

    #endregion

    #region Public Methods

    public override void RegisterTransitions(BaseStateMachine<KongController> stateMachine)
    {
        var run = stateMachine.GetStateByType(typeof(KongRunState));
        var airborn = stateMachine.GetStateByType(typeof(KongAirbornMachine));
        var attackToStand = stateMachine.GetStateByType(typeof(KongAttackToStandState));

        AddTransition(run, new CompositePredicate(
            new IPredicate[] {
                new AnimationPredicate(animator, KongController.Animations.Attack, AnimationPredicate.Timing.End),
                new FunctionPredicate(() => controller.Run)
            }
        ));
        AddTransition(airborn, new FunctionPredicate(() => controller.Jump));
        AddTransition(attackToStand, new AnimationPredicate(animator, KongController.Animations.Attack, AnimationPredicate.Timing.End));
    }

    #endregion

    #region State Events

    public override void OnStateStart()
    {
        controller.EnableGravity();
        controller.Damageable.EnableInvulnerability(true);
        controller.AttackDamager.enabled = true;

        animator.Play(KongController.Animations.Attack);
    }

    public override void OnStateUpdate()
    {
        controller.PerformVelocityHorizontalMovement(KongController.Instance.MovementSettings.RunSpeed);
    }

    public override void OnStateExit()
    {
        controller.Damageable.DisableInvulnerability();
        controller.AttackDamager.enabled = false;
    }

    #endregion
}