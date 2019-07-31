using UnityEditor;
using UnityEngine;

/// <summary>
/// Constrols the auto barrel
/// </summary>
public class AutoBarrel : BlastBarrel
{
    #region State Variables

    /// <summary>
    /// Defines the direction the barrel is rotating
    /// </summary>
    [HideInInspector]
    public float Direction
    {
        get => animator.GetFloat("Direction");
        set => animator.SetFloat("Direction", value);
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// The target direction to blast the player
    /// </summary>
    [Tooltip("The target direction to blast the player.")]
    public int TargetFrame;

    #endregion

    #region Unity Methods

    private new void Awake()
    {
        base.Awake();

        // Start state machine
        SceneSMB<AutoBarrel>.Initialise(animator, this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // Set full state
            Full = true;

            // Change to new state
            SetState("aim", StartFrame);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // Set full state
            Full = false;
        }
    }

    protected new void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();

        var direction = Vector2.zero;

        // Calculate points
        if (Application.isPlaying)
            direction = Force * PhysicsTime * GetBlastDirection(TargetFrame);
        else
            direction = Force * PhysicsTime * GetBlastDirection(TargetFrame);

        var point = direction + (Vector2)transform.position;

        // Draw the gizmo
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, point);
        Gizmos.DrawWireSphere(point, 0.1f);
        Handles.Label(point, "Target Blast");
    }

    #endregion

    #region Check Methods

    /// <summary>
    /// Check if the aim movement has finished
    /// </summary>
    /// <returns></returns>
    public bool CheckForAimFinish() => animator.GetCurrentNormalizedFrame(AnimationLayer) == TargetFrame;

    /// <summary>
    /// Check if the recoil movement has finished
    /// </summary>
    /// <returns></returns>
    public bool CheckForRecoilFinish() => animator.GetCurrentNormalizedFrame(AnimationLayer) == StartFrame;

    #endregion

    #region Perform Methods

    /// <summary>
    /// Returns the barrel to the idle state
    /// </summary>
    public void PerformIdle() => SetState("idle", StartFrame);

    /// <summary>
    /// Gets the direction to aim
    /// </summary>
    public void PerformAimDirection()
    {
        // Get the current Frame
        var currentFrame = animator.GetCurrentNormalizedFrame(AnimationLayer);

        // Find the shortest direction
        Direction = GetShortestDirection(currentFrame, TargetFrame);
    }

    /// <summary>
    /// Gets the direction to recoil
    /// </summary>
    public void PerformRecoilDirection()
    {
        // Get the current frame
        var currentFrame = animator.GetCurrentNormalizedFrame(AnimationLayer);

        // Gets the shortest direction for the target
        Direction = GetShortestDirection(currentFrame, StartFrame);
    }

    /// <summary>
    /// Performs the blast, forcing the player out of the barrel
    /// </summary>
    public void PerformAutoBlast()
    {
        // Find player child
        var kongController = transform.GetComponentInChildren<KongController>();
        if (kongController != null)
        {
            // Set player state
            kongController.Blast = true;

            // Set internal state
            SetState("recoil", animator.GetCurrentNormalizedFrame(AnimationLayer));
        }
    }

    /// <summary>
    /// Performs the player executed blast, when player hits blast befores the timer runs out
    /// </summary>
    public override void PerformPlayerBlast()
    {
        // Set internal state
        SetState("recoil", animator.GetCurrentFrame(AnimationLayer));
    }

    #endregion
}
