using UnityEngine;

public class ThrowSettings : MonoBehaviour {
    /// <summary>
    /// The offset where the sprite should snap as a <see cref="Vector3"/>
    /// </summary>
    public Vector3 SnapOffset = Vector3.zero;

    /// <summary>
    /// The force applied on the item when performing a normal throw
    /// </summary>
    [Tooltip("The force applied on the item when performing a normal throw")]
    public Vector2 NormalThrowForce = Vector2.zero;

    /// <summary>
    /// The force applied on the item when performing a up throw
    /// </summary>
    [Tooltip("The force applied on the item when performing a up throw")]
    public Vector2 UpThrowForce = Vector2.zero;
}