using UnityEngine;

public partial class KongController
{
    #region Public Properties

    [HideInInspector]
    public Vector2 DeathDirection;

    #endregion

    #region Public Methods

    public void PerformDeathJump()
    {
        Debug.Log(DeathDirection);
        Debug.Log(new Vector2(DamagerSettings.DeathJumpForce.x * DeathDirection.x, DamagerSettings.DeathJumpForce.y));
        // Makes the player jump dies
        mRigidBody2D.AddForce(new Vector2(DamagerSettings.DeathJumpForce.x * DeathDirection.x, DamagerSettings.DeathJumpForce.y));
    }

    #endregion
}
