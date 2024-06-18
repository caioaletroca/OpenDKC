using UnityEngine;

public partial class KongController
{
    #region Unity Methods

    /// <summary>
    /// Handles the collision on a hook
    /// </summary>
    /// <param name="collision"></param>
    public void OnBarrelTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Barrel")
        {
            Barrel = true;
            Blast = false;
            BarrelTrigger();
            SetParent(collision.gameObject);
        }
    }

    /// <summary>
    /// Handles the collision exit on a hook
    /// </summary>
    /// <param name="collision"></param>
    public void OnBarrelTriggerExit2D(Collider2D collision)
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
        // Reset trigger
        // TODO: Discover a better way to solve that problem
        animator.ResetTrigger(AnimationParameters.BarrelTrigger);

        // Advice barrel about the blast
        barrel.PerformPlayerBlast();

        // Resets velocity for more sensitivy movement
        SetVelocity(Vector2.zero);

        // Create jump force
        mRigidBody2D.AddForce(barrel.BlastDirection * barrel.Velocity, ForceMode2D.Impulse);
    }

    #endregion
}
