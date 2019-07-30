using UnityEngine;

/// <summary>
/// Controls the default blast barrel
/// </summary>
public class BlastBarrel : MonoBehaviour
{
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

    #endregion

    #region Unity Methods

    private void OnDrawGizmosSelected()
    {
        // Calculate points
        var direction = Force * PhysicsTime * transform.up;
        var point = direction + transform.position;

        // Draw the gizmo
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, point);
        Gizmos.DrawWireSphere(point, 0.1f);
    }

    #endregion
}
