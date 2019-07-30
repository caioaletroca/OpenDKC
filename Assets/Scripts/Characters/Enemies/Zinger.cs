using UnityEngine;

/// <summary>
/// Controls the zinger enemy
/// </summary>
public class Zinger : Enemy
{
    #region Unity Methods

    private new void Awake()
    {
        base.Awake();

        // Enables damager
        Damager.Enable();
    }

    #endregion
}
