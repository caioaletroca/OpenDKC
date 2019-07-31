using UnityEngine;

/// <summary>
/// Controls the rotative barrel
/// </summary>
[RequireComponent(typeof(Animator))]
public class RotativeBarrel : BlastBarrel
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
    /// The amount of time the barrel will auto shoot
    /// </summary>
    [Tooltip("The amount of time the barrel will auto shoot.")]
    public int BlastTime = 5;

    /// <summary>
    /// Instance for the number icon
    /// </summary>
    [Tooltip("Instance for the Number Icon.")]
    public UnitIntSpriteRenderer NumberIcon;

    #endregion

    #region Private Properties

    /// <summary>
    /// The timer to store the blast maximum time
    /// </summary>
    protected float BlastTimer;

    /// <summary>
    /// A flag that represents if a blast timer request was made
    /// </summary>
    private bool updateBlastTimerRequest;

    /// <summary>
    /// The last saved direction for the barrel rotation
    /// </summary>
    private float LastDirection = 0;

    #endregion

    #region Unity Methods

    private new void Awake()
    {
        base.Awake();

        // Start state machine
        SceneSMB<RotativeBarrel>.Initialise(animator, this);

        // Set the direction to be always to the right
        LastDirection = Direction = 1;
    }

    private void Start() => NumberIcon.Value = BlastTime;

    private void FixedUpdate()
    {
        UpdateBlastTimer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // Set full state
            Full = true;
            
            // Change to new state
            SetState("rotate", StartFrame);

            BlastTimer = BlastTime;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // Set full state
            Full = false;

            // Reset counter icon
            NumberIcon.Value = BlastTime;
        }
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

    #region Check Methods

    /// <summary>
    /// Check if the blaster timer has finished
    /// </summary>
    /// <returns></returns>
    public bool CheckForBlastTimer() => BlastTimer < 0;

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
    /// Performs blast timer count
    /// Makes a request to update the blaster timer on the FixedUpdate
    /// </summary>
    public void PerformBlastTimer() => updateBlastTimerRequest = true;

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
            SetState("recoil", animator.GetCurrentFrame(AnimationLayer));
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

    #region Update Methods

    /// <summary>
    /// Updates the blaster timer with a request
    /// </summary>
    public void UpdateBlastTimer()
    {
        // Check for update request
        if (!updateBlastTimerRequest)
            return;

        // Calculate time
        BlastTimer -= Time.fixedDeltaTime;
        NumberIcon.Value = (int)Mathf.Round(BlastTimer);

        // Disable update request flag
        updateBlastTimerRequest = false;
    }

    /// <summary>
    /// Updates the <see cref="Direction"/> state variable
    /// </summary>
    public void UpdateDirection()
    {
        // Get the current input type
        var input = InputController.Instance.HorizontalValue;

        if (InputController.Instance.SideButton)
        {
            Direction = 0; return;
        }
        else
            Direction = LastDirection;

        // Set the direction
        if (Direction < 0 && input > 0)
            LastDirection = Direction = 1;
        else if (Direction > 0 && input < 0)
            LastDirection = Direction = -1;
    }

    /// <summary>
    /// Updates the direction looking for the shortest way
    /// </summary>
    public void UpdateRecoilDirection()
    {
        // Get the current frame
        var currentFrame = animator.GetCurrentNormalizedFrame(AnimationLayer);

        // Gets the shortest direction for the target
        Direction = GetShortestDirection(currentFrame, StartFrame);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Sets the Barrel state
    /// </summary>
    /// <param name="name">The name of the next state</param>
    /// <param name="frame">The frame to start</param>
    protected void SetState(string name, int frame = 0) => animator.Play(name, AnimationLayer, GetNormalizedTime(frame));

    #endregion
}
