using UnityEngine;

/// <summary>
/// Defines extensions methods for the <see cref="Animator"/>
/// </summary>
public static class AnimatorExtensions
{
    /// <summary>
    /// Gets the current frame of animation from a defined layer
    /// </summary>
    /// <param name="animator">The animator</param>
    /// <param name="layer">The specified layer</param>
    /// <returns></returns>
    public static int GetCurrentFrame(this Animator animator, int layer)
    {
        // Get information
        var animationClip = animator.GetCurrentAnimatorClipInfo(layer);
        var animationState = animator.GetCurrentAnimatorStateInfo(layer);

        // Calculate frame
        var floatFrame = animationClip[0].clip.length * (animationState.normalizedTime % 1) * animationClip[0].clip.frameRate;

        // Convert data
        return (int)Mathf.Floor(floatFrame);
    }

    /// <summary>
    /// Gets the current normalized time of an animation from a defined layer
    /// </summary>
    /// <param name="animator">The animator</param>
    /// <param name="layer">The specified layer</param>
    /// <returns></returns>
    public static float GetCurrentNormalizedTime(this Animator animator, int layer) => animator.GetCurrentAnimatorStateInfo(layer).normalizedTime;

    /// <summary>
    /// Get the current total of frames of an animation from a defined layer
    /// </summary>
    /// <param name="animator">The animator</param>
    /// <param name="layer">The specified layer</param>
    /// <returns></returns>
    public static int GetCurrentTotalFrames(this Animator animator, int layer)
    {
        // Get data
        var animationClip = animator.GetCurrentAnimatorClipInfo(1)[0].clip;

        // Calculate frames
        var totalFrames = animationClip.length * animationClip.frameRate;

        // Convert data
        return (int)Mathf.Floor(totalFrames);
    }
}
