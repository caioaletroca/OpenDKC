using System.Collections;
using UnityEngine;
using UnityEngine.Animations;

/// <summary>
/// Creates a delay for Enter event for the <see cref="SceneSMB{TMonoBehaviour}"/> class
/// </summary>
/// <typeparam name="TMonoBehaviour"></typeparam>
public class DelayedSceneSMB<TMonoBehaviour> : SceneSMB<TMonoBehaviour>
    where TMonoBehaviour : MonoBehaviour
{
    #region Public Properties

    /// <summary>
    /// The delay to start this behaviour
    /// </summary>
    [Tooltip("The amount of time to delay the enter execution.")]
    public float DelayTime = 1;

    #endregion

    #region State Methods

    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.StartCoroutine(DelayCoroutine(DelayTime, animator, stateInfo, layerIndex));
    }

    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        mMonoBehaviour.StartCoroutine(DelayCoroutine(DelayTime, animator, stateInfo, layerIndex, controller));
    }

    #endregion

    #region Delayed Methods

    /// <summary>
    /// <see cref="OnSLStateEnter(Animator, AnimatorStateInfo, int)"/> fired after a delay time
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="stateInfo"></param>
    /// <param name="layerIndex"></param>
    public virtual void OnSLStateEnterDelayed(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) { }

    /// <summary>
    /// <see cref="OnSLStateEnter(Animator, AnimatorStateInfo, int)"/> fired after a delay time
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="stateInfo"></param>
    /// <param name="layerIndex"></param>
    /// <param name="controller"></param>
    public virtual void OnSLStateEnterDelayed(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller) { }

    #endregion

    #region Private Methods

    /// <summary>
    /// Delays the Enter event for a amount of time in seconds
    /// </summary>
    /// <param name="delay"></param>
    /// <param name="animator"></param>
    /// <param name="stateInfo"></param>
    /// <param name="layerIndex"></param>
    /// <returns></returns>
    private IEnumerator DelayCoroutine(float delay, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Delay the time in seconds
        yield return new WaitForSeconds(delay);

        // Rise events
        OnSLStateEnterDelayed(animator, stateInfo, layerIndex);
    }

    /// <summary>
    /// Delays the Enter event for a amount of time in seconds
    /// </summary>
    /// <param name="delay"></param>
    /// <param name="animator"></param>
    /// <param name="stateInfo"></param>
    /// <param name="layerIndex"></param>
    /// <param name="controller"></param>
    /// <returns></returns>
    private IEnumerator DelayCoroutine(float delay, Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        // Delay the time in seconds
        yield return new WaitForSeconds(delay);

        // Rise events
        OnSLStateEnterDelayed(animator, stateInfo, layerIndex, controller);
    }

    #endregion
}
