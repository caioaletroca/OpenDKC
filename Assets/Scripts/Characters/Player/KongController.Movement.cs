using UnityEngine;

public partial class KongController
{
    #region Type Definitions

    /// <summary>
    /// Defines a set of variables to handle an axis movement
    /// </summary>
    private class MovementAxis {
        /// <summary>
        /// Defines which axis this structured corresponds
        /// </summary>
        public Vector2 Axis;

        /// <summary>
        /// Stores the movement velocity
        /// </summary>
        public float Movement = 0;

        /// <summary>
        /// A flag that represents if there's a velocity driven request
        /// </summary>
        public bool VelocityDriven = false;

        /// <summary>
        /// Stores the movement force
        /// </summary>
        public float Force = 0;

        /// <summary>
        /// A flag that represents if there's a force driven request
        /// </summary>
        public bool ForceDriven = false;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Movement"></param>
        /// <param name="Force"></param>
        /// <param name="VelocityDriven"></param>
        /// <param name="ForceDriven"></param>
        public MovementAxis(Vector2 Axis) {
            this.Axis = Axis;
        }
    }

    #endregion

    #region Private Properties

    /// <summary>
    /// The radius for the overlap calculations on the grounded check
    /// </summary>
    const float mGroundedRadius = 0.2f;

    private MovementAxis X = new(Vector2.right);

    private MovementAxis Y = new(Vector2.up);

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
    /// Perform the normal jump
    /// </summary>
    public void PerformJump() => PerformVerticalImpulse(MovementSettings.JumpForce);

    public void PerformVelocityHorizontalMovement(float Speed) =>
        PerformVelocityMovement(X, Speed * InputController.Instance.HorizontalValue);

    public void PerformForceHorizontalMovement(float Magnitude) =>
        PerformForceMovement(X, Magnitude * InputController.Instance.HorizontalValue);

    public void PerformVelocityVerticalMovement(float Speed) =>
        PerformVelocityMovement(Y, Speed * InputController.Instance.VerticalValue);

    public void PerformForceVerticalMovement(float Magnitude) =>
        PerformForceMovement(Y, Magnitude * InputController.Instance.VerticalValue);

    public void PerformHorizontalImpulse(float Magnitude) {
        // Resets velocity to make the movement more apealling
        mRigidBody2D.velocity = new Vector2(0, mRigidBody2D.velocity.y);

        // Add horizontal force to the player
        PerformImpulse(new Vector2(Magnitude, 0));
    }

    public void PerformVerticalImpulse(float Magnitude) {
        // Resets velocity to make the movement more apealling
        mRigidBody2D.velocity = new Vector2(mRigidBody2D.velocity.x, 0);

        // Add vertical force to the player
        PerformImpulse(new Vector2(0, Magnitude));
    }    

    #endregion

    #region Protected Methods

    /// <summary>
    /// Fixed Update for all movement related mechanics.
    /// Should be called by main FixedUpdate function.
    /// </summary>
    protected void UpdateMovement() {
        UpdateVelocityMovement(X);
        UpdateForceMovement(X);
        UpdateVelocityMovement(Y);
        UpdateForceMovement(Y);

        UpdateTerminalVelocity();
        CheckForFlip();
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Set a new movement speed on axis
    /// </summary>
    /// <param name="axis">Axis to be changed</param>
    /// <param name="Speed">The new speed</param>
    private void PerformVelocityMovement(MovementAxis axis, float Speed) {
        // Set the request as true
        axis.VelocityDriven = true;

        // Set the movement
        axis.Movement = Speed;
    }

    /// <summary>
    /// Set a new movement force on axis
    /// </summary>
    /// <param name="axis">Axis to be changed</param>
    /// <param name="Magnitude">Force magnitude</param>
    private void PerformForceMovement(MovementAxis axis, float Magnitude) {
        // Set the request as true
        axis.ForceDriven = true;

        // Set the force
        axis.Force = Magnitude * InputController.Instance.HorizontalValue;
    }

    /// <summary>
    /// Performs a impulse on RigidBody2D
    /// </summary>
    /// <param name="Force">Force applied</param>
    private void PerformImpulse(Vector2 Force) {
        // Add vertical force to the player
        mRigidBody2D.AddForce(Force, ForceMode2D.Impulse);
    }

    /// <summary>
    /// Updates the movement using forces
    /// </summary>
    private void UpdateVelocityMovement(MovementAxis axis) {
        // Check if a movement request was made
        if (!axis.VelocityDriven)
            return;

        // Normal movement
        TargetVelocity =
            // Assign a new velocity filtered out the current working axis
            (axis.Axis * axis.Movement * 10f * Time.deltaTime) +
            // Get the other axis (perpendicular) and filter the current true velocity from RigidBody2D
            (Vector2.Perpendicular(axis.Axis) * mRigidBody2D.velocity);

        // Apply smoothing to the movement
        mRigidBody2D.velocity = Vector2.SmoothDamp(mRigidBody2D.velocity, TargetVelocity, ref mVelocity, MovementSettings.MovementSmoothing);        

        // Turn off the request flag
        axis.VelocityDriven = false;
    }

    /// <summary>
    /// Updates the movement using forces
    /// </summary>
    private void UpdateForceMovement(MovementAxis axis)
    {
        // Check if a movement request was made
        if (!axis.ForceDriven)
            return;

        // Apply smoothing to the movement
        mRigidBody2D.AddForce(axis.Axis * axis.Force, ForceMode2D.Force);

        // Turn off the request flag
        axis.ForceDriven = false;
    }

    /// <summary>
    /// Checks and flips game object if needed
    /// </summary>
    private void CheckForFlip() {
        // Face the player to the right side depending on his movement
        if (X.Movement > 0 && !mFacingRight)
            Flip();
        else if (X.Movement < 0 && mFacingRight)
            Flip();
    }

    /// <summary>
    /// Update sequence for the Vertical Movement
    /// </summary>
    private void UpdateTerminalVelocity()
    {
        // Set terminal velocity
        if (mRigidBody2D.velocity.y >= MovementSettings.MaximumVelocityY)
            mRigidBody2D.velocity = new Vector2(mRigidBody2D.velocity.x, MovementSettings.MaximumVelocityY);
    }

    #endregion
}
