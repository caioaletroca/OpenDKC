using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    #region Public Properties

    public float StartingHealth = 5;

    public bool InvulnerableAfterDamage = true;

    public float InvulnerabilityDuration = 3f;

    public bool DisableOnDeath;

    public float CurrentHealth;

    #endregion

    #region Private Methods

    public bool Invulnerable;

    #endregion

    #region Unity Methods

    private void OnEnable()
    {
        CurrentHealth = StartingHealth;
    }

    private void Update()
    {
        if (Invulnerable)
        {

        }
    }

    #endregion
}
