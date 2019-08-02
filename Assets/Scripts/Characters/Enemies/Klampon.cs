using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Controls the bitten enemy
/// </summary>
public class Klampon : Neek
{
    #region State Variable

    /// <summary>
    /// Represents if klampon has bitten the player
    /// </summary>
    public void BiteTrigger() => animator.SetTrigger("BiteTrigger");

    #endregion
}

