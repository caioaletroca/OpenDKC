using UnityEngine;

/// <summary>
/// Controls the zinger enemy
/// </summary>
public class Zinger : Enemy
{
    #region Public Properties

    /// <summary>
    /// The instance for the damager
    /// </summary>
    public Damager Damager;

    [Header("Movement Settings")]

    /// <summary>
    /// The amplitude for the movement
    /// </summary>
    [Tooltip("The amplitude for the zig zag movement")]
    public float Amplitude = 0.5f;

    /// <summary>
    /// The speed for the movement
    /// </summary>
    [Tooltip("The speed for the movement")]
    public Vector2 Speed = new Vector2(1, 1);

    #endregion

    #region Private Properties

    /// <summary>
    /// The original starting position
    /// </summary>
    private Vector2 OriginalPosition = Vector2.zero;

    #endregion

    #region Unity Methods

    private new void Awake()
    {
        base.Awake();

        // Enables damager
        Damager.Enable();
    }

    void Start()
    {
        OriginalPosition = transform.position;
    }

    private void FixedUpdate()
    {
        // Uses sine waves to create the movement
        var newX = OriginalPosition.x + (Mathf.Sin(Time.time * Speed.x) * Amplitude);
        var newY = OriginalPosition.y + (Mathf.Sin(Time.time * Speed.y + Mathf.PI / 4) * Amplitude);
        
        // Creates the new position
        var newPosition = new Vector3(newX, newY, transform.position.z);
        
        // Set the new position
        transform.position = newPosition;
    }

    #endregion
}
