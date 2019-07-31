using UnityEngine;
using UnityEngine.Animations;

/// <summary>
/// A Base class for the behaviours in the game using <see cref="StateMachineBehaviour"/> as engine
/// </summary>
/// <typeparam name="TMonoBehaviour">The state machine</typeparam>
public class SceneSMB<TMonoBehaviour> :  SealedSMB
    where TMonoBehaviour : MonoBehaviour
{
    #region Private Properties

    /// <summary>
    /// A instance for the current <see cref="MonoBehaviour"/>
    /// </summary>
    protected TMonoBehaviour mMonoBehaviour;

    /// <summary>
    /// A flag that represents if the first frame was occurred
    /// </summary>
    private bool mFirstFrame;

    /// <summary>
    /// A flag that represents if the last frame was occurred
    /// </summary>
    private bool mLastFrame;

    #endregion

    #region Initialization Methods

    public static void Initialise(Animator animator, TMonoBehaviour monoBehaviour)
    {
        foreach (var sceneSMB in animator.GetBehaviours<SceneSMB<TMonoBehaviour>>())
            sceneSMB.InternalInitialise(animator, monoBehaviour);
    }

    protected void InternalInitialise(Animator animator, TMonoBehaviour monoBehaviour)
    {
        mMonoBehaviour = monoBehaviour;
        OnStart(animator);
    }

    #endregion

    #region Inherited Methods

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        mFirstFrame = false;

        OnSLStateEnter(animator, stateInfo, layerIndex);
        OnSLStateEnter(animator, stateInfo, layerIndex, controller);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        // Avoid if the animator was disabled
        if (!animator.gameObject.activeSelf)
            return;

        if(animator.IsInTransition(layerIndex) && animator.GetNextAnimatorStateInfo(layerIndex).fullPathHash == stateInfo.fullPathHash)
        {
            OnSLTransitionToStateUpdate(animator, stateInfo, layerIndex);
            OnSLTransitionToStateUpdate(animator, stateInfo, layerIndex, controller);
        }

        if (!animator.IsInTransition(layerIndex) && mFirstFrame)
        {
            OnSLStateNoTransitionUpdate(animator, stateInfo, layerIndex);
            OnSLStateNoTransitionUpdate(animator, stateInfo, layerIndex, controller);
        }

        if (animator.IsInTransition(layerIndex) && !mLastFrame && mFirstFrame)
        {
            mLastFrame = true;

            OnSLStatePreExit(animator, stateInfo, layerIndex);
            OnSLStatePreExit(animator, stateInfo, layerIndex, controller);
        }

        if (!animator.IsInTransition(layerIndex) && !mFirstFrame)
        {
            mFirstFrame = true;

            OnSLStatePostEnter(animator, stateInfo, layerIndex);
            OnSLStatePostEnter(animator, stateInfo, layerIndex, controller);
        }

        if (animator.IsInTransition(layerIndex) && animator.GetCurrentAnimatorStateInfo(layerIndex).fullPathHash == stateInfo.fullPathHash)
        {
            OnSLTransitionFromStateUpdate(animator, stateInfo, layerIndex);
            OnSLTransitionFromStateUpdate(animator, stateInfo, layerIndex, controller);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        mLastFrame = false;

        OnSLStateExit(animator, stateInfo, layerIndex);
        OnSLStateExit(animator, stateInfo, layerIndex, controller);
    }

    #endregion

    #region State Events

    /// <summary>
    /// Called by a <see cref="MonoBehaviour"/> in the scene during its Start function
    /// </summary>
    /// <param name="animator"></param>
    public virtual void OnStart(Animator animator) { }

    /// <summary>
    /// Called before Updates when execution of the state first starts (on transition to the state).
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="stateInfo"></param>
    /// <param name="layerIndex"></param>
    public virtual void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

    /// <summary>
    /// Called before Updates when execution of the state first starts (on transition to the state).
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="stateInfo"></param>
    /// <param name="layerIndex"></param>
    /// <param name="controller"></param>
    public virtual void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller) { }

    /// <summary>
    /// Called after <see cref="OnSLStateEnter(Animator, AnimatorStateInfo, int)"/> every frame during transition to the state
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="stateInfo"></param>
    /// <param name="layerIndex"></param>
    public virtual void OnSLTransitionToStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

    /// <summary>
    /// Called after <see cref="OnSLStateEnter(Animator, AnimatorStateInfo, int)"/> every frame during transition to the state
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="stateInfo"></param>
    /// <param name="layerIndex"></param>
    /// <param name="controller"></param>
    public virtual void OnSLTransitionToStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller) { }

    /// <summary>
    /// Called on the first from after the transition to the state has finished
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="stateInfo"></param>
    /// <param name="layerIndex"></param>
    public virtual void OnSLStatePostEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

    /// <summary>
    /// Called on the first from after the transition to the state has finished
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="stateInfo"></param>
    /// <param name="layerIndex"></param>
    /// <param name="controller"></param>
    public virtual void OnSLStatePostEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller) { }

    /// <summary>
    /// Called every frame after <see cref="OnSLStatePostEnter(Animator, AnimatorStateInfo, int)"/> when the state is not being transitioned to or from
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="stateInfo"></param>
    /// <param name="layerIndex"></param>
    public virtual void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

    /// <summary>
    /// Called every frame after <see cref="OnSLStatePostEnter(Animator, AnimatorStateInfo, int)"/> when the state is not being transitioned to or from
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="stateInfo"></param>
    /// <param name="layerIndex"></param>
    /// <param name="controller"></param>
    public virtual void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller) { }

    /// <summary>
    /// Called on the first frame after the transition from the state has started. NOTE: If the transition has a duration of less than a frame, it will not be fired
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="stateInfo"></param>
    /// <param name="layerIndex"></param>
    public virtual void OnSLStatePreExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

    /// <summary>
    /// Called on the first frame after the transition from the state has started. NOTE: If the transition has a duration of less than a frame, it will not be fired
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="stateInfo"></param>
    /// <param name="layerIndex"></param>
    /// <param name="controller"></param>
    public virtual void OnSLStatePreExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller) { }

    /// <summary>
    /// Called after <see cref="OnSLStatePreExit(Animator, AnimatorStateInfo, int)"/> every frame during the trasition to the state
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="stateInfo"></param>
    /// <param name="layerIndex"></param>
    public virtual void OnSLTransitionFromStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

    /// <summary>
    /// Called after <see cref="OnSLStatePreExit(Animator, AnimatorStateInfo, int)"/> every frame during the trasition to the state
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="stateInfo"></param>
    /// <param name="layerIndex"></param>
    /// <param name="controller"></param>
    public virtual void OnSLTransitionFromStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller) { }

    /// <summary>
    /// Called after Updates when execution of the state first finishes (after trasition from the state)
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="stateInfo"></param>
    /// <param name="layerIndex"></param>
    public virtual void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

    /// <summary>
    /// Called after Updates when execution of the state first finishes (after trasition from the state)
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="stateInfo"></param>
    /// <param name="layerIndex"></param>
    /// <param name="controller"></param>
    public virtual void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller) { }

    #endregion
}

/// <summary>
/// Base sealed class for the State Behaviours
/// </summary>
public abstract class SealedSMB : StateMachineBehaviour
{
    public sealed override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

    public sealed override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

    public sealed override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }
}