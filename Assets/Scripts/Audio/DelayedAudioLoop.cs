using UnityEngine;

/// <summary>
/// Executes a audio source every amount of time with delay
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class DelayedAudioLoop : MonoBehaviour
{
    #region Public Properties

    /// <summary>
    /// The elapsed time to delay between loops
    /// </summary>
    [Tooltip("The elapsed time to delay the sound effect between loops")]
    public float LoopDelay = 4;

    #endregion

    #region Private Properties

    /// <summary>
    /// Instance for the audio source
    /// </summary>
    protected AudioSource audioSource;

    /// <summary>
    /// Stores the timer to execute the sound
    /// </summary>
    protected float AudioTimer;

    #endregion

    #region Unity Methods

    private void Awake() => audioSource = GetComponent<AudioSource>();

    private void Update()
    {
        // Decrement timer
        AudioTimer -= Time.deltaTime;

        if (AudioTimer < 0)
        {
            // Restart timer
            AudioTimer = LoopDelay;

            // Play sound
            audioSource.Play();
        }
    }

    #endregion
}
