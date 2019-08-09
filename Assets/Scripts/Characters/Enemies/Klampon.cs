using UnityEngine;

/// <summary>
/// Controls the bitten enemy
/// </summary>
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(PlatformMovement))]
public class Klampon : Enemy
{
    #region State Variable

    /// <summary>
    /// Represents if klampon has bitten the player
    /// </summary>
    public bool Bite
    {
        get => animator.GetBool("Bite");
        set => animator.SetBool("Bite", value);
    }

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

    #endregion

    #region Unity Methods

    protected new void Awake()
    {
        base.Awake();

        audioSource = GetComponent<AudioSource>();
        platformMovement = GetComponent<PlatformMovement>();

        // Start state machine
        SceneSMB<Klampon>.Initialise(animator, this);
    }

    #endregion

    #region Perform Methods

    /// <summary>
    /// Performs the bite sound effect
    /// </summary>
    public void PerformBiteSound() => audioSource.Play();

    #endregion
}

