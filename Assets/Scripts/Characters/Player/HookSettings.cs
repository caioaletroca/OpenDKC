using UnityEditor;
using UnityEngine;

/// <summary>
/// Defines settings for when the player is hanging on a Hook
/// </summary>
public class HookSettings : MonoBehaviour
{
    /// <summary>
    /// The offset where the sprite should snap as a <see cref="Vector3"/>
    /// </summary>
    public Vector3 SnapOffset = Vector3.zero;

    /// <summary>
    /// The force applied on the Dismount Jump
    /// </summary>
    [Tooltip("The force applied on the Dismount Jump")]
    public float DismountJumpForce = 1;

    
}
