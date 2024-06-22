using UnityEngine;

/// <summary>
/// Handles the settings configuration for Movement
/// </summary>
public class MovementSettings : MonoBehaviour
{
    public float WalkSpeed = 100f;

    public float RunSpeed = 200f;

    public float AirSpeed = 100f;

    public float CrouchSpeed = 50f;

    [Range(0, 0.3f)]
    public float MovementSmoothing = 0.3f;

    /// <summary>
    /// The force magnitude applied to the X value when force driven movement
    /// </summary>
    [Tooltip("The force magnitude applied to the X value when force driven movement.")]
    public float ForceMagnitude = 5f;

    public float JumpForce = 100f;

    /// <summary>
    /// Force applied to perform a Attack movement
    /// </summary>
    [Tooltip("Force applied to perform a Attack movement when realized from idle state")]
    public float AttackForce = 5f;

    public float MaximumVelocityY = 100;

    /// <summary>
    /// The knock back force magnitude
    /// </summary>
    [Tooltip("The force applied to the player when knocked back by a enemy.")]
    public Vector2 KnockBackForce = new Vector2(1, 1);

    /// <summary>
    /// A <see cref="LayerMask"/> for the ground collision
    /// </summary>
    public LayerMask GroundLayer;

    /// <summary>
    /// A empty game object that represents the ground calculation point
    /// </summary>
    public Transform GroundPoint;
}
