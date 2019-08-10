using UnityEngine;

/// <summary>
/// Handles the Bullet projetile object
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Damager))]
public class Bullet : MonoBehaviour
{
    #region Public Methods

    /// <summary>
    /// Instance for the bullet pool object
    /// </summary>
    [HideInInspector]
    public BulletObject bulletPoolObject;

    #endregion

    #region Event Methods

    public void OnDeactivateEvent()
    {
        // Deactivate the bullet to return to pool
        bulletPoolObject.ReturnToPool();
    }

    #endregion
}
