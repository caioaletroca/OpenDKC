using UnityEngine;

public class RopeSettings : MonoBehaviour {
    /// <summary>
    /// Horizontal speed for rope climbing
    /// </summary>
    [Tooltip("Horizontal speed for rope climbing.")]
    public float HorizontalSpeed = 5;

    /// <summary>
    /// Vertical speed for rope climbing
    /// </summary>
    [Tooltip("Vertical speed for rope climbing.")]
    public float VerticalSpeed = 5;
}