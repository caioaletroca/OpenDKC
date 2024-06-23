using UnityEditor;
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
    /// Velocity which the player will be thrown
    /// </summary>
    [Tooltip("Defines the velocity blast of the barrel")]
    public float Velocity = 0;

    /// <summary>
    /// The amount of time the gravity will be off after the barrel blast
    /// </summary>
    [Tooltip("The amount of time the gravity will be off after the barrel blast.")]
    public float HangTime = 1;

    /// <summary>
    /// The start direction the barrel will face
    /// </summary>
    [Tooltip("Defines the default barrel facing side.")]
    public int StartDirection = 0;

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
        PerformFrame(StartDirection);
        PerformBlastDirection(StartDirection);
    }

    protected void OnDrawGizmosSelected()
    {
        var blastDirection = Application.isPlaying ? BlastDirection : GetBlastDirection(StartDirection);

        // Draw the gizmo
        Gizmos.color = Color.green;
        if(HangTime > 0) {
            var blastEnd = Velocity * HangTime * blastDirection + (Vector2)transform.position;

            DrawDirection("Blast End", blastEnd);
        }
        else {
            var direction = blastDirection + (Vector2)transform.position;

            DrawDirection("Blast Direction", direction);
        }
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

    #region Protected Methods

    /// <summary>
    /// Sets the Barrel state
    /// </summary>
    /// <param name="name">The name of the next state</param>
    /// <param name="frame">The frame to start</param>
    protected void SetState(string name, int frame = 0) => animator.Play(name, AnimationLayer, GetNormalizedTime(frame));

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

    /// <summary>
    /// Calculates the shortest direction rotation to arrive a target frame
    /// </summary>
    /// <param name="startDirection">The start frame</param>
    /// <param name="targetFrame">The target frame</param>
    /// <returns></returns>
    protected int GetShortestDirection(int startDirection, int targetFrame)
    {
        // Get the current frame
        var animationLength = animator.GetCurrentTotalFrames(AnimationLayer);

        // Updates the shortest direction
        var distance = (targetFrame - startDirection + 2 * animationLength) % animationLength - animationLength / 2;

        // Return the direction
        return distance >= 0 ? -1 : 1;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="frame"></param>
    /// <returns></returns>
    protected int NormalizeFrame(int frame) => frame >= 0 ? frame : frame + animator.GetCurrentTotalFrames(AnimationLayer);

    #endregion

    #region Debug Methods

    protected void DrawDirection(string label, Vector2 direction) {
        Gizmos.DrawLine(transform.position, direction);
        Gizmos.DrawWireSphere(direction, 0.1f);
        Handles.Label(direction + new Vector2(0.1f, 0), label);
    }

    #endregion
}
