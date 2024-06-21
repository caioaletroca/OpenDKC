using UnityEngine;

public class AnimationPredicate : IPredicate
{
    #region Types

    public enum Timing {
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

        return (
            currentState.shortNameHash == animationHash &&
            currentState.normalizedTime > GetTiming(timing)
        );
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