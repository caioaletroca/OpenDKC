using UnityEngine;

public partial class KongController
{
    #region Public Methods

    /// <summary>
    /// Handles the collision on a hook
    /// </summary>
    /// <param name="collision"></param>
    public void OnBarrelTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Barrel")
        {
            Barrel = true;
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
        }
    }

    #endregion
}
