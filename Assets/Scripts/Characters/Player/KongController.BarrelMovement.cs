using UnityEngine;

public partial class KongController
{
    #region Event Methods

    /// <summary>
    /// Handles the collision on a hook
    /// </summary>
    /// <param name="collision"></param>
    private void OnBarrelTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Barrel" && !Barrel)
        {
            Barrel = true;
            SetParent(collision.gameObject);
        }
    }

    /// <summary>
    /// Handles the collision exit on a hook
    /// </summary>
    /// <param name="collision"></param>
    private void OnBarrelTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Barrel")
        {
            Barrel = false;
            SetParent(null);
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Performs the blast jump from a barrel
    /// </summary>
    /// <param name="barrel">The barrel</param>
    public void PerformBarrelBlast(BlastBarrel barrel)
    {
        // Advice barrel about the blast
        barrel.PerformPlayerBlast();

        // Resets velocity for more sensitivy movement
        SetVelocity(Vector2.zero);

        // Create jump force
        mRigidBody2D.AddForce(barrel.BlastDirection * barrel.Velocity, ForceMode2D.Impulse);
    }

    #endregion
}
