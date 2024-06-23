using System.Collections;
using UnityEngine;

public class KongBlastState : BaseState<KongController> {
    #region Private Properties

    /// <summary>
    /// A flag that represents if the gravity has been enabled again or not
    /// </summary>
    public bool GravityEnabled = false;

    /// <summary>
    /// Stores the gravity coroutine
    /// </summary>
    public Coroutine co;

    #endregion

    #region Constructor

    public KongBlastState(KongController controller, Animator animator) : base(controller, animator) { }

    #endregion

    #region Public Methods

    public override void RegisterTransitions(BaseStateMachine<KongController> stateMachine)
    {
        var idle = stateMachine.GetState(typeof(KongIdleState));
        var walk = stateMachine.GetState(typeof(KongWalkState));
        var run = stateMachine.GetState(typeof(KongRunState));
        var hook = stateMachine.GetState(typeof(KongHookStateMachine));
        var insideBarrel = stateMachine.GetState(typeof(KongInsideBarrelState));

        AddTransition(idle, new FunctionPredicate(() => controller.Grounded && controller.HorizontalValue < 0.001));
        AddTransition(walk, new FunctionPredicate(() => controller.Grounded && controller.HorizontalValue > 0.001 && !controller.Run));
        AddTransition(run, new FunctionPredicate(() => controller.Grounded && controller.HorizontalValue > 0.001 && controller.Run));
        AddTransition(hook, new FunctionPredicate(() => controller.Hook));
        AddTransition(insideBarrel, new FunctionPredicate(() => controller.Barrel && !controller.Blast));
    }

    #endregion

    #region State Events

    public override void OnStateStart()
    {
        // Set state variable
        GravityEnabled = false;
        
        // Get the Barrel Entity
        var barrel = controller.GetComponentInParent<BlastBarrel>();

        // Blast player
        controller.PerformBarrelBlast(barrel);

        // Start the gravity coroutine
        co = controller.StartCoroutine(TurnGravityDelay(barrel.HangTime));

        animator.Play(KongController.Animations.Somersault);
    }

    public override void OnStateFixedUpdate()
    {
        // Enable force driven movement on fall
        if (GravityEnabled)
            controller.PerformForceHorizontalMovement(controller.MovementSettings.ForceMagnitude);
    }

    public override void OnStateExit()
    {
        controller.StopCoroutine(co);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Turns gravity on with a delay
    /// </summary>
    /// <param name="delay">The delayed time</param>
    /// <returns></returns>
    private IEnumerator TurnGravityDelay(float delay)
    {
        // Wait for the delay
        yield return new WaitForSeconds(delay);

        // Re-enables gravity for player
        controller.EnableGravity();

        // Set the state variable
        GravityEnabled = true;
    }

    #endregion
}