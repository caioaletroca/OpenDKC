using UnityEngine;

/// <summary>
/// General damager settings for the player controller
/// </summary>
public class DamagerSettings : MonoBehaviour
{
    [Tooltip("The duration for the attack in seconds. It applies both for collision detection and for the state machine itself.")]
    public float AttackDuration = 0.667f;
}
