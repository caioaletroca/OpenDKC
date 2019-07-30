using UnityEngine;

/// <summary>
/// Controls the floating motion for flying objects
/// </summary>
public class AirMotion : MonoBehaviour
{
    #region Public Properties

    /// <summary>
    /// The amplitude for the movement
    /// </summary>
    [Tooltip("The amplitude for the zig zag movement.")]
    public Vector2 Amplitude = new Vector2(0.05f, 0.05f);

    /// <summary>
    /// The speed for the movement
    /// </summary>
    [Tooltip("The speed for the movement.")]
    public Vector2 Speed = new Vector2(1, 1);

    /// <summary>
    /// A flag that represents if the object should flip when turning directions
    /// </summary>
    [Tooltip("If enabled, the object will flip when change direction.")]
    public bool MovementFlip = false;

    /// <summary>
    /// The local speed for the movement
    /// </summary>
    [HideInInspector]
    public Vector2 Velocity = Vector2.zero;

    #endregion

    #region Private Properties

    /// <summary>
    /// The original starting position
    /// </summary>
    private Vector2 OriginalPosition = Vector2.zero;

    /// <summary>
    /// A flag that represents the direction the object is facing to
    /// </summary>
    private bool FacingDirection;

    #endregion

    #region Unity Methods

    void Start()
    {
        OriginalPosition = transform.position;
    }

    private void FixedUpdate()
    {
        // Uses sine waves to create the movement
        var newX = OriginalPosition.x + (Mathf.Sin(Time.time * Speed.x) * Amplitude.x);
        var newY = OriginalPosition.y + (Mathf.Sin(Time.time * Speed.y + Mathf.PI / 4) * Amplitude.y);

        // Calculate the current velocity
        Velocity = (new Vector2(newX, newY) - (Vector2)transform.position) / Time.fixedDeltaTime;

        // Flip if enabled
        if (MovementFlip)
        {
            if (Velocity.x >= 0 && !FacingDirection || Velocity.x < 0 && FacingDirection)
                Flip();
        }

        // Creates the new position
        var newPosition = new Vector3(newX, newY, transform.position.z);

        // Set the new position
        transform.position = newPosition;
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Flips the sprite for the object
    /// </summary>
    protected void Flip()
    {
        FacingDirection = !FacingDirection;

        // Multiply the x local scale by -1
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    #endregion
}
