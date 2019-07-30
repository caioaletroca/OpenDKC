using System.Collections;
using UnityEngine;

/// <summary>
/// Constrols the blasting state for the player controller
/// </summary>
public class BlastingSMB : SceneSMB<KongController>
{
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

    #region State Methods

    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Set state variable
        GravityEnabled = false;
        
        // Get the Barrel Entity
        var barrel = mMonoBehaviour.GetComponentInParent<BlastBarrel>();

        // Blast player
        mMonoBehaviour.BlastFromBarrel(barrel);

        // Start the gravity coroutine
        co = mMonoBehaviour.StartCoroutine(TurnGravityDelay(barrel.PhysicsTime));
    }

    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        // Enable force driven movement on fall
        if (GravityEnabled)
            mMonoBehaviour.PerformForceHorizontalMovement(mMonoBehaviour.MovementSettings.ForceMagnitude);
    }

    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.StopCoroutine(co);
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
        mMonoBehaviour.EnableGravity();

        // Set the state variable
        GravityEnabled = true;
    }

    #endregion
}
