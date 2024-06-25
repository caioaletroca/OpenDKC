using UnityEngine;

public class AnimationPredicate : IPredicate
{
    #region Types

    public enum Timing {
        Start,
        Playing,
        End
    }

    #endregion

    #region Private Properties

    /// <summary>
    /// Animator instance
    /// </summary>
    readonly Animator animator;

    /// <summary>
    /// Animation hash, as returned from Animator.StringToHash
    /// </summary>
    readonly int animationHash;

    /// <summary>
    /// Timing for the trigger
    /// </summary>
    readonly Timing timing;

    #endregion

    #region Constructor

    public AnimationPredicate(Animator animator, int animationHash, Timing timing)
    {
        this.animator = animator;
        this.animationHash = animationHash;
        this.timing = timing;
    }

    #endregion

    #region Public Methods

    public bool Evaluate() {
        var currentState = animator.GetCurrentAnimatorStateInfo(0);

        // Return false if the desired animation is not playing
        if(currentState.shortNameHash != animationHash)
            return false;

        // If the desired timing is Start or End, calculate it and return
        if(timing != Timing.Playing) {
            return currentState.normalizedTime > GetTiming(timing);
        }

        // If not, the evaluation is only about if the desired animation is playing,
        // and at this point, it is.
        return true;
    }

    #endregion

    #region Private Methods

    float GetTiming(Timing timing) {
        if(timing == Timing.End) {
            return 1.0f;
        }

        return 0;
    }

    #endregion
}