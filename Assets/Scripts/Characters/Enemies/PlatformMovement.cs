using UnityEngine;

/// <summary>
/// Constrols the platforming movement for enemies
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlatformMovement : MonoBehaviour
{
    #region Public Properties

    /// <summary>
    /// The walking speed for the object
    /// </summary>
    public float Speed = 5;

    /// <summary>
    /// The look direction for the cliff detection system
    /// </summary>
    [Tooltip("Look direction for the cliff detection system.")]
    public Vector2 LookDirection;

    /// <summary>
    /// The layers to check for the cliff detection
    /// </summary>
    [Tooltip("The layers to check for the cliff detection.")]
    public LayerMask GroundLayer;

    /// <summary>
    /// The layer to check for the wall to turn back
    /// </summary>
    [Tooltip("The layer to check for the wall to turn back.")]
    public LayerMask WallLayer;

    #endregion

    #region Private Properties

    /// <summary>
    /// A instance for the <see cref="Rigidbody2D"/> for the game Object
    /// </summary>
    protected Rigidbody2D mRigidBody2D;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        mRigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var direction = transform.localScale.x * -1;

        // Set speed
        mRigidBody2D.velocity = new Vector2(direction * Speed, mRigidBody2D.velocity.y);

        // Check if should turn on cliff
        var realDirection = new Vector2(LookDirection.x * transform.localScale.x, LookDirection.y);
        var hit = Physics2D.Raycast(transform.position, realDirection, LookDirection.magnitude, GroundLayer);
        if (hit.collider == null)
            Flip();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Controls the flip behaviour when hitting a wall
        if (WallLayer.Contains(collision.gameObject))
            Flip();
    }

    private void OnDrawGizmosSelected()
    {
        // Calculate points
        var realDirection = new Vector2(LookDirection.x * transform.localScale.x, LookDirection.y);
        var endPosition = realDirection + (Vector2)transform.position;

        // Draw gizmo
        Gizmos.DrawLine(transform.position, endPosition);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Flips the sprite for the object
    /// </summary>
    protected void Flip()
    {
        // Multiply the x local scale by -1
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    #endregion
}
