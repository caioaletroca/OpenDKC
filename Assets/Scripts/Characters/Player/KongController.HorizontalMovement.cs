using UnityEngine;

public partial class KongController
{
    #region Private Properties

    /// <summary>
    /// Stores the horizontal movement velocity
    /// </summary>
    private Vector2 Movement = Vector2.zero;

    /// <summary>
    /// A stored value for the current velocity
    /// </summary>
    private Vector2 TargetVelocity = Vector2.zero;

    /// <summary>
    /// Used for velocity calculation
    /// </summary>
    private Vector2 mVelocity = Vector3.zero;

    #endregion

    #region Public Methods

    /// <summary>
    /// Set a new horizontal moviment
    /// </summary>
    /// <param name="Speed">The new speed</param>
    public void PerformHorizontalMovement(float Speed) => Movement = new Vector2(Speed * PlayerInput.Instance.HorizontalValue, 0);

    #endregion

    #region Private Methods

    /// <summary>
    /// Updates the Movement Sequence
    /// </summary>
    protected void UpdateHorizontalMovement()
    {
        // Normal movement
        TargetVelocity = new Vector2(Movement.x * 10f * Time.deltaTime, mRigidBody2D.velocity.y);

        // Face the player to the right side depending on his movement
        if (Movement.x > 0 && !mFacingRight)
            Flip();
        else if (Movement.x < 0 && mFacingRight)
            Flip();

        // Apply smoothing to the movement
        mRigidBody2D.velocity = Vector2.SmoothDamp(mRigidBody2D.velocity, TargetVelocity, ref mVelocity, MovementSettings.MovementSmoothing);
    }

    #endregion
}
