using UnityEngine;

/// <summary>
/// Handles the instantiation for VFX using the <see cref="VFXController"/> as in State transitions
/// </summary>
public class TriggerVFX : StateMachineBehaviour
{
    #region Public Properties

    /// <summary>
    /// The triggered prefab name
    /// </summary>
    [Tooltip("Uses the same name as the prefab for the vfx.")]
    public string VfxName;

    /// <summary>
    /// The offset where the vfx will spawn
    /// </summary>
    [Tooltip("The offset where the vfx will spawn.")]
    public Vector3 Offset = Vector3.zero;

    /// <summary>
    /// A flag that represents if the current vfx should be attached to his parent
    /// </summary>
    [Tooltip("If this vfx should follow his parent.")]
    public bool AttachToParent = false;

    [Tooltip("The amount of time to delay the vfx start.")]
    public float StartDelay = 0;

    /// <summary>
    /// A flag that represents if the vfx should play at the state start
    /// </summary>
    [Tooltip("If the vfx should play at the state start.")]
    public bool OnEnter = true;

    /// <summary>
    /// A flag that represents if the vfx should play at the state end
    /// </summary>
    [Tooltip("If the vfx should play at the state end.")]
    public bool OnExit = false;

    #endregion

    #region State Methods

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (OnEnter)
            Trigger(animator.transform);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (OnExit)
            Trigger(animator.transform);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Triggers the current vfx using <see cref="VFXController"/>
    /// </summary>
    /// <param name="transform"></param>
    protected void Trigger(Transform transform)
    {
        var flip = false;
        var spriteRenderer = transform.GetComponent<SpriteRenderer>();
        if (spriteRenderer)
            flip = spriteRenderer.flipX;

        // Trigger the effect
        VFXController.Instance.Trigger(VfxName, Offset + transform.position, StartDelay, flip, AttachToParent ? transform : null);
    }

    #endregion
}
