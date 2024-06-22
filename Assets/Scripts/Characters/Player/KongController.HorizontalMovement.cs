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

    /// <summary>
    /// A flag that represents if there's a velocity driven request
    /// </summary>
    private bool VelocityDriven = false;

    /// <summary>
    /// Stores the horizontal movement force
    /// </summary>
    private Vector2 Force = Vector2.zero;

    /// <summary>
    /// A flag that represents if there's a force driven request
    /// </summary>
    private bool ForceDriven = false;

    #endregion

    #region Public Methods

    /// <summary>
    /// Set a new horizontal moviment
    /// </summary>
    /// <param name="Speed">The new speed</param>
    public void PerformVelocityHorizontalMovement(float Speed)
    {
        // Set the request as true
        VelocityDriven = true;

        // Set the movement
        Movement = new Vector2(Speed * InputController.Instance.HorizontalValue, 0);
    }

    public void PerformForceHorizontalMovement(float Magnitude)
    {
        // Set the request as true
        ForceDriven = true;

        // Set the force
        Force = new Vector2(Magnitude * InputController.Instance.HorizontalValue, 0);
    }

    public void PerformIdleAttack() {
        mRigidBody2D.velocity = new Vector2(0, mRigidBody2D.velocity.y);

        mRigidBody2D.AddForce(new Vector2(mFacingRight ? MovementSettings.AttackForce : -MovementSettings.AttackForce, 0), ForceMode2D.Impulse);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Updates the Movement using velocity
    /// </summary>
    protected void UpdateVelocityHorizontalMovement()
    {
        // Check if a movement request was made
        if (!VelocityDriven)
            return;

        // Normal movement
        TargetVelocity = new Vector2(Movement.x * 10f * Time.deltaTime, mRigidBody2D.velocity.y);

        // Face the player to the right side depending on his movement
        if (Movement.x > 0 && !mFacingRight)
            Flip();
        else if (Movement.x < 0 && mFacingRight)
            Flip();

        // Apply smoothing to the movement
        mRigidBody2D.velocity = Vector2.SmoothDamp(mRigidBody2D.velocity, TargetVelocity, ref mVelocity, MovementSettings.MovementSmoothing);

        // Turn off the request flag
        VelocityDriven = false;
    }

    /// <summary>
    /// Updates the movement using forces
    /// </summary>
    protected void UpdateForceHorizontalMovement()
    {
        // Check if a movement request was made
        if (!ForceDriven)
            return;

        // Face the player to the right side depending on his movement
        if (Force.x > 0 && !mFacingRight)
            Flip();
        else if (Force.x < 0 && mFacingRight)
            Flip();

        // Apply smoothing to the movement
        mRigidBody2D.AddForce(Force, ForceMode2D.Force);

        // Turn off the request flag
        ForceDriven = false;
    }

    #endregion
}
