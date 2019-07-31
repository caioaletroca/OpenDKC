using UnityEngine;

/// <summary>
/// Controls the default blast barrel
/// </summary>
[RequireComponent(typeof(Animator))]
public class BlastBarrel : MonoBehaviour
{
    #region State Variable

    /// <summary>
    /// A flag that represents if the player is inside the barrel
    /// </summary>
    [HideInInspector]
    public bool Full
    {
        get => animator.GetBool("Full");
        set => animator.SetBool("Full", value);
    }

    /// <summary>
    /// The idle normalized time
    /// </summary>
    [HideInInspector]
    public float NormalizedTime
    {
        get => animator.GetFloat("NormalizedTime");
        set => animator.SetFloat("NormalizedTime", value);
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// The power the barrel will blast the player
    /// </summary>
    [Tooltip("The power the barrel will blast the player.")]
    public float Force = 1;

    /// <summary>
    /// The amount of time the gravity will be off after the barrel blast
    /// </summary>
    [Tooltip("The amount of time the gravity will be off after the barrel blast.")]
    public float PhysicsTime = 1;

    /// <summary>
    /// The start direction the barrel will face
    /// </summary>
    [Tooltip("Defines the default barrel facing side.")]
    public int StartFrame = 0;

    /// <summary>
    /// The Direction the player will be blasted
    /// </summary>
    [HideInInspector]
    public Vector2 BlastDirection = Vector2.up;

    #endregion

    #region Private Properties

    /// <summary>
    /// Instance for the animator
    /// </summary>
    protected Animator animator;

    /// <summary>
    /// The used animation layer
    /// </summary>
    protected int AnimationLayer = 0;

    #endregion

    #region Unity Methods

    protected void Awake() => animator = GetComponent<Animator>();

    private void Start()
    {
        PerformFrame(StartFrame);
        PerformBlastDirection(StartFrame);
    }

    private void OnDrawGizmosSelected()
    {
        // Calculate points
        var direction = Force * PhysicsTime * BlastDirection;
        var point = direction + (Vector2)transform.position;

        // Draw the gizmo
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, point);
        Gizmos.DrawWireSphere(point, 0.1f);
    }

    #endregion

    #region Update Methods

    /// <summary>
    /// Updates the blaster direction using the current frame
    /// </summary>
    public void UpdateBlastDiretion()
    {
        // Update blast direction with the current animation frame
        BlastDirection = GetBlastDirection(animator.GetCurrentFrame(AnimationLayer));
    }

    #endregion

    #region Perform Methods

    /// <summary>
    /// Performs the normalized time using frames
    /// </summary>
    /// <param name="frame"></param>
    public void PerformFrame(int frame) => PerformNormalizedTime(GetNormalizedTime(frame));

    /// <summary>
    /// Performs a new normalized time
    /// </summary>
    /// <param name="normalizedTime"></param>
    public void PerformNormalizedTime(float normalizedTime) => NormalizedTime = normalizedTime;

    /// <summary>
    /// Performs the new blast direction using a frame
    /// </summary>
    /// <param name="frame"></param>
    public void PerformBlastDirection(int frame) => BlastDirection = GetBlastDirection(frame);

    /// <summary>
    /// Performs the player executed blast, when player hits blast befores the timer runs out
    /// </summary>
    public virtual void PerformPlayerBlast() { }

    #endregion

    #region Private Methods

    /// <summary>
    /// Get the current blast direction with the current animation frame
    /// </summary>
    /// <param name="frame">The current animation frame</param>
    /// <returns></returns>
    protected Vector2 GetBlastDirection(int frame)
    {
        // Calculate direction
        var directionX = Mathf.Sin(frame * Mathf.PI / 8);
        var directionY = Mathf.Cos(frame * Mathf.PI / 8);

        return new Vector2(directionX, directionY);
    }

    /// <summary>
    /// Gets the normalized time for a specified frame with the current animation length
    /// </summary>
    /// <param name="frame">The specified frame</param>
    /// <returns></returns>
    protected float GetNormalizedTime(int frame) => frame / (float)animator.GetCurrentTotalFrames(AnimationLayer);

    #endregion
}
