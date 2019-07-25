using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handles the damage received from a <see cref="Damager"/> class
/// </summary>
public class Damageable : MonoBehaviour, IDataPersister
{
    #region Types

    /// <summary>
    /// Default health value changes or updates event
    /// </summary>
    [Serializable]
    public class HealtEvent : UnityEvent<Damageable> { }

    /// <summary>
    /// Default damage event
    /// </summary>
    [Serializable]
    public class DamageEvent : UnityEvent<Damager, Damageable> { }

    #endregion

    #region Public Properties

    /// <summary>
    /// The starting amount of health
    /// </summary>
    [Tooltip("The starting health amount.")]
    public float StartingHealth = 5;

    /// <summary>
    /// A flag that represents if the object will be temporary invencible after damaged
    /// </summary>
    [Tooltip("If enabled, the object will be temporary invencible after damaged.")]
    public bool InvulnerableAfterDamage = true;

    /// <summary>
    /// The duration for the invulnerability time
    /// </summary>
    [Tooltip("The duration for the invulnerability time.")]
    public float InvulnerabilityDuration = 3f;

    /// <summary>
    /// A flag that represents if the game object should be disabled after dies
    /// </summary>
    [Tooltip("If enabled, the object will be disabled after dies.")]
    public bool DisableOnDeath;

    /// <summary>
    /// An offset from the obejct position used to set from where the distance to the damager is computed
    /// </summary>
    [Tooltip("An offset from the obejct position used to set from where the distance to the damager is computed.")]
    public Vector2 CentreOffset = Vector2.zero;

    /// <summary>
    /// The current health for the game object
    /// </summary>
    [HideInInspector]
    public float Health
    {
        get { return mHealth; }
        set
        {
            // Checks if the new value is different
            if (mHealth != value)
            {
                // First, update the value
                mHealth = value;

                // Fire the event
                OnHealthChanged?.Invoke(this);
            }
        }
    }

    /// <summary>
    /// The direction the damage was received
    /// </summary>
    [HideInInspector]
    public Vector2 DamageDirection;

    /// <summary>
    /// The instance for the data persistent settings
    /// </summary>
    [HideInInspector]
    public DataSettings dataSettings;

    #endregion

    #region Private Methods

    /// <summary>
    /// Internal health value
    /// </summary>
    protected float mHealth;

    /// <summary>
    /// Represents if the object is invencible or not
    /// </summary>
    protected bool Invulnerable;

    /// <summary>
    /// Control variable to determine the invencible time
    /// </summary>
    protected float InvulnerabilityTimer;

    #endregion

    #region Events

    /// <summary>
    /// Fires when the <see cref="Health"/> value changes
    /// </summary>
    public HealtEvent OnHealthChanged;

    /// <summary>
    /// Fires whenever the game object receives a damager hit
    /// </summary>
    public DamageEvent OnTakeDamage;

    /// <summary>
    /// Fires when the game object run outs of health and dies
    /// </summary>
    public DamageEvent OnDie;

    #endregion

    #region Unity Methods

    private void OnEnable()
    {
        // Register persistence
        PersistentDataManager.RegisterPersister(this);

        // Set initial health value
        Health = StartingHealth;

        DisableInvulnerability();
    }

    private void OnDisable() => PersistentDataManager.UnregisterPersister(this);

    private void Update()
    {
        if (Invulnerable)
        {
            InvulnerabilityTimer -= Time.deltaTime;
            if (InvulnerabilityTimer <= 0f)
                Invulnerable = false;
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Enables the invulnerability
    /// </summary>
    /// <param name="ignoreTimer">If enabled, makes invulnerability permanent</param>
    public void EnableInvulnerability(bool ignoreTimer = false)
    {
        Invulnerable = true;

        // Technically don't ignore timer, just set it to an insanly big number. Allow to avoid to add more test & special case.
        InvulnerabilityTimer = ignoreTimer ? float.MaxValue : InvulnerabilityDuration;
    }

    /// <summary>
    /// Disables the invulnerability
    /// </summary>
    public void DisableInvulnerability() => Invulnerable = false;

    /// <summary>
    /// Takes damage from a <see cref="Damager"/> object
    /// </summary>
    /// <param name="damager">The damager who attacked</param>
    /// <param name="ignoreInvincible">If should ignore the <see cref="Damageable"/> invencibility status</param>
    public void TakeDamage(Damager damager, bool ignoreInvincible = false)
    {
        if ((Invulnerable && !ignoreInvincible) || Health <= 0)
            return;

        // Find the damage direction
        var damageDirection = transform.position + ((Vector3)CentreOffset - damager.transform.position);
        DamageDirection = damageDirection / damageDirection.magnitude;

        // Reduce health and apply damage
        if(!Invulnerable)
            Health -= damager.Damage;

        // Fire event
        OnTakeDamage?.Invoke(damager, this);

        // Check if the game object has died
        if(Health <= 0)
        {
            OnDie?.Invoke(damager, this);
            EnableInvulnerability();
            if (DisableOnDeath) gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Gains a determined amount of health
    /// </summary>
    /// <param name="amount">The amount to add</param>
    public void GainHealth(int amount)
    {
        // Caps the maximum health to the starting value
        if (Health + amount > StartingHealth)
            Health = StartingHealth;
        else
            Health += amount;
    }

    #endregion

    #region Data Persister Methods

    public DataSettings GetDataSettings() => dataSettings;

    public void SetDataSettings(string dataTag, DataSettings.PersistenceType persistenceType)
    {
        dataSettings.dataTag = dataTag;
        dataSettings.persistenceType = persistenceType;
    }

    public Data SaveData() => new Data<float>(Health);

    public void LoadData(Data data) => Health = ((Data<float>)data).value;

    #endregion
}
