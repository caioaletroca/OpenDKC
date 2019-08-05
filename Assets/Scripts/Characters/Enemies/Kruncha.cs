using UnityEngine;

public class Kruncha : Klomp
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

    #endregion

    #region Private Properties

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
        Damager.Enable();

        // Sets the timer
        RageTimer = RageTime;
    }

    #endregion

    #region Perform Methods

    /// <summary>
    /// Plays the step sound effect
    /// </summary>
    public void PerformStepSound() => audioSource.Play();

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
            Damager.Disable();

            // Set the initial value
            RageTimer = RageTime;
        }
    }

    #endregion
}