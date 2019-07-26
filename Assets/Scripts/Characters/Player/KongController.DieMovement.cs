using UnityEngine;

public partial class KongController
{
    #region Public Properties

    [HideInInspector]
    public Vector2 DeathDirection;

    #endregion

    #region Public Methods

    /// <summary>
    /// Performs the death jump effect, when the player gets hit by the enemy
    /// </summary>
    public void PerformDeathJump()
    {
        // Resets velocity to better create movement with forces
        mRigidBody2D.velocity = Vector2.zero;

        // Makes the player jump dies
        mRigidBody2D.AddForce(new Vector2(DamagerSettings.DeathJumpForce.x * DeathDirection.x, DamagerSettings.DeathJumpForce.y), ForceMode2D.Impulse);
    }

    public void PerformDeathBounce()
    {
        var factor = 0.5f;
        
        // Makes the player bounces dies
        mRigidBody2D.AddForce(new Vector2(DamagerSettings.DeathJumpForce.x * DeathDirection.x, DamagerSettings.DeathJumpForce.y) * factor, ForceMode2D.Impulse);
    }

    #endregion
}
