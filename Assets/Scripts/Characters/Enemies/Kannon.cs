using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Constrols the Kannon enemy
/// </summary>
public class Kannon : Enemy
{
    #region Public Properties



    #endregion

    #region Private Properties

    /// <summary>
    /// Instance for the bullet pool
    /// </summary>
    protected BulletPool bulletPool;

    #endregion

    #region Unity Methods

    protected new void Awake()
    {
        base.Awake();

        bulletPool = GetComponent<BulletPool>();
    }

    #endregion
}
