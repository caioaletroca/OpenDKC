using UnityEngine;
/// <summary>
/// Handles the barrel projectile from kannon
/// </summary>
public class KannonBarrel : KannonBall
{
    #region Public Properties

    /// <summary>
    /// The layers the projectile will collide and will be destroyed
    /// </summary>
    [Tooltip("The layers the projectile will collide and will be destroyed.")]
    public LayerMask CollisionLayer;

    #endregion

    #region Unity Methods

    protected void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if(CollisionLayer.Contains(collision.gameObject))
        {
            Explode();
        }
    }

    #endregion

    #region Event Methods

    public void OnDamageableEvent(Damager damager, Damageable damageable)
    {
        Explode();
    }

    #endregion

    #region Public Methods

    public void Explode()
    {
        // Return object to the pool
        bulletPoolObject.ReturnToPool();
    }

    #endregion
}