using UnityEngine;

/// <summary>
/// Stores the Damage settings for the player controller
/// </summary>
public class DamageSettings : MonoBehaviour
{
    /// <summary>
    /// The force for the jump when the player dies
    /// </summary>
    public Vector2 DeathJumpForce = new Vector2(25, 200);

    /// <summary>
    /// The death music played when dies
    /// </summary>
    [Tooltip("The music to play when the player gets hit and dies.")]
    public AudioClip DeathMusic;

    /// <summary>
    /// The amount of time to wait before the restart level
    /// </summary>
    [Tooltip("The amount of time to wait before the restart level.")]
    public float RestartDelay;
}
