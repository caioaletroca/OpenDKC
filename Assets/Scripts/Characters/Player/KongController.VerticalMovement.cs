using UnityEngine;

public partial class KongController
{
    #region Public Methods

    /// <summary>
    /// Perform the normal jump
    /// </summary>
    public void PerformJump()
    {
        // Resets velocity to make the jump more apealling
        mRigidBody2D.velocity = new Vector2(mRigidBody2D.velocity.x, 0);

        // Add vertical force to the player
        mRigidBody2D.AddForce(new Vector2(0f, MovementSettings.JumpForce), ForceMode2D.Impulse);
    }

    public void PerformHookDismount() {
        // Resets velocity to make the jump more apealling
        mRigidBody2D.velocity = new Vector2(mRigidBody2D.velocity.x, 0);

        // Add vertical force to the player
        mRigidBody2D.AddForce(new Vector2(0f, HookSettings.DismountJumpForce), ForceMode2D.Impulse);
    }

    #endregion

    #region Private Properties

    /// <summary>
    /// The radius for the overlap calculations on the grounded check
    /// </summary>
    const float mGroundedRadius = 0.2f;

    #endregion

    #region Private Methods

    /// <summary>
    /// Update sequence for the Vertical Movement
    /// </summary>
    public void UpdateVerticalMovement()
    {
        // Set terminal velocity
        if (mRigidBody2D.velocity.y >= MovementSettings.MaximumVelocityY)
            mRigidBody2D.velocity = new Vector2(mRigidBody2D.velocity.x, MovementSettings.MaximumVelocityY);
    }

    #endregion
}

