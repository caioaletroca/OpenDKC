using UnityEngine;

/// <summary>
/// Controls the klomp enemy
/// </summary>
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(PlatformMovement))]
public class Klomp : Enemy
{
    #region Private Properties

    /// <summary>
    /// Instance for the audio source
    /// </summary>
    protected AudioSource audioSource;

    /// <summary>
    /// Instance for the platform movement
    /// </summary>
    protected PlatformMovement platformMovement;

    #endregion

    #region Unity Methods

    protected new void Awake()
    {
        base.Awake();

        audioSource = GetComponent<AudioSource>();
        platformMovement = GetComponent<PlatformMovement>();
    }

    #endregion

    #region Event Methods

    public override void OnDieEvent(Damager damager, Damageable damageable)
    {
        // Set state variable
        Die = true;

        // Stop movement
        platformMovement.enabled = false;

        DisableEnemy();
        PerformDeathJump(damageable.DamageDirection);
        
        // Despawn in time
        Destroy(gameObject, TimeToDespawn);
    }

    public override void OnActivateEvent()
    {
        base.OnActivateEvent();

        platformMovement.enabled = true;
    }

    public override void OnDeactivateEvent()
    {
        base.OnDeactivateEvent();
        
        platformMovement.enabled = false;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Fires when the animation hits the klomp step time
    /// </summary>
    public void OnStepEvent()
    {
        // Produces step sound
        audioSource.Play();
    }

    #endregion
}
