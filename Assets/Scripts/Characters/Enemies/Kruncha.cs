using UnityEngine;

/// <summary>
/// Controls the Kruncha enemy
/// </summary>
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(PlatformMovement))]
public class Kruncha : Enemy
{
    #region State Variables

    /// <summary>
    /// Represents if the object has died
    /// </summary>
    public bool Rage
    {
        get => animator.GetBool("Rage");
        set => animator.SetBool("Rage", value);
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// The amount of time the object stays on the rage mode
    /// </summary>
    [Tooltip("The amount of time the object stays on the rage mode")]
    public float RageTime = 1;

    /// <summary>
    /// The speed for the normal state
    /// </summary>
    [Tooltip("The speed for the normal state.")]
    public float NormalSpeed = 1;

    /// <summary>
    /// The speed for the rage state
    /// </summary>
    [Tooltip("The speed for the rage state.")]
    public float RageSpeed = 2;

    #endregion

    #region Private Properties

    /// <summary>
    /// Instance for the audio source
    /// </summary>
    protected AudioSource audioSource;

    /// <summary>
    /// Instance for the platform movement
    /// </summary>
    protected PlatformMovement platformMovement;

    /// <summary>
    /// Stores the current timer value
    /// </summary>
    protected float RageTimer;

    #endregion

    #region Unity Methods

    private new void Awake()
    {
        base.Awake();

        audioSource = GetComponent<AudioSource>();
        platformMovement = GetComponent<PlatformMovement>();

        // Disable Damager
        Damager.enabled = false;

        // Set default speed
        platformMovement.Speed = NormalSpeed;
    }

    private void Start()
    {
        RageTimer = RageTime;
    }

    private void Update()
    {
        UpdateRageTimer();
    }

    #endregion

    #region Event Methods

    /// <summary>
    /// Fires when the player hits the object
    /// </summary>
    public void OnRageEvent()
    {
        // Avoid double rage
        if (Rage)
            return;

        // Set variable
        Rage = true;

        // Turn on damager
        Damager.enabled = true;

        // Stop Movement
        platformMovement.enabled = false;
        platformMovement.Speed = RageSpeed;

        // Sets the timer
        RageTimer = RageTime;
    }

    #endregion

    #region Perform Methods

    /// <summary>
    /// Plays the step sound effect
    /// </summary>
    public void PerformStepSound() => audioSource.Play();

    public void PerformEnableMovement() => platformMovement.enabled = true;

    #endregion

    #region Update Methods

    /// <summary>
    /// Updates the rage timer
    /// </summary>
    public void UpdateRageTimer()
    {
        // Do not update if not on rage mode
        if (!Rage)
            return;

        // Decrement the timer
        RageTimer -= Time.deltaTime;

        if(RageTimer < 0)
        {
            // Resets flag
            Rage = false;

            // Disables damager
            Damager.enabled = false;

            // Set the initial value
            RageTimer = RageTime;

            // Set normal speed
            platformMovement.Speed = NormalSpeed;
        }
    }

    #endregion
}