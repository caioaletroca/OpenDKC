using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handles the damager interation with the <see cref="Damageable"/> class
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class Damager : MonoBehaviour
{
    #region Types

    /// <summary>
    /// Event for damaging hits for the <see cref="Damager"/> class
    /// </summary>
    [Serializable]
    public class DamageableEvent : UnityEvent<Damager, Damageable> { }

    /// <summary>
    /// Event for no damaging hits for the <see cref="Damager"/> class
    /// </summary>
    [Serializable]
    public class NonDamageableEvent : UnityEvent<Damager> { }

    #endregion

    #region Public Properties

    /// <summary>
    /// The total damage inflicted on enemies
    /// </summary>
    [Tooltip("The total damage inflicted on enemies.")]
    public int Damage = 1;

    /// <summary>
    /// A flag that represents if the damager should hit trigger colliders
    /// </summary>
    [Tooltip("If disabled, damager ignore trigger when casting for damage.")]
    public bool CanHitTrigger = false;

    /// <summary>
    /// A flag that represents if the damager will disable automatically after the first hit
    /// </summary>
    [Tooltip("If enabled, the damager will disable automatically after the first hit.")]
    public bool DisableAfterHit = false;
    
    /// <summary>
    /// A flag that represents if the damager should ignore the damageable invulnerable status
    /// </summary>
    [Tooltip("If set, an invincible damageable hit will still get the onHit message (but won't loose any life).")]
    public bool IgnoreInvincibility = false;

    /// <summary>
    /// The layer the damager will check for hits
    /// </summary>
    [Tooltip("The layer available the damager to hit.")]
    public LayerMask HittableLayers;

    /// <summary>
    /// A flag that represents if the game object can hit another <see cref="Damageable"/> if hits from above
    /// </summary>
    [Tooltip("If enabled, the object will hit another if bounced above, like platform games as Super Mario.")]
    public bool HitFromAbove = false;

    #endregion

    #region Events

    /// <summary>
    /// Fired when damager makes a hit with damage
    /// </summary>
    public DamageableEvent OnDamageableHit;

    /// <summary>
    /// Fired when damagers makes a above hit with damage
    /// </summary>
    public DamageableEvent OnBounceDamageableHit;

    /// <summary>
    /// Fired when damager makes a hit without damage
    /// </summary>
    public NonDamageableEvent OnNonDamageableHit;

    #endregion

    #region Unity Methods

    protected void Update() { }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        // Do not execute if disabled
        if (!enabled)
            return;
        
        // Check for the right layer
        if (!HittableLayers.Contains(collision.gameObject))
            return;

        // Get the damageable component instance
        var damageable = collision.gameObject.GetComponent<Damageable>();
        if (damageable && damageable.enabled)
            TakeDamage(damageable);
        else
            OnNonDamageableHit?.Invoke(this);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        // Do not execute if disabled
        if (!enabled)
            return;

        // Check for the right layer
        if (!HittableLayers.Contains(collision.gameObject))
            return;

        // Get the damageable component instance
        var damageable = collision.gameObject.GetComponent<Damageable>();
        if (damageable && damageable.enabled)
            TakeDamage(damageable);
        else
            OnNonDamageableHit?.Invoke(this);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Computes the damage for a <see cref="Damageable"/> component
    /// </summary>
    /// <param name="damageable">The component who takes the hit</param>
    protected void TakeDamage(Damageable damageable)
    {
        // Calculate the collision direction
        var damageDirection = (damageable.transform.position + ((Vector3)damageable.CentreOffset - transform.position)).normalized;

        // Check if this damager makes hits from above
        if (HitFromAbove)
        {
            // Check for hit from above
            if (damageDirection.y * -1 > Mathf.Abs(damageDirection.x))
            {
                OnDamageableHit?.Invoke(this, damageable);
                OnBounceDamageableHit?.Invoke(this, damageable);
                damageable.TakeDamage(this, IgnoreInvincibility);
                if (DisableAfterHit)
                    enabled = false;
            }
        }
        else
        {
            // Fire normal damage
            OnDamageableHit?.Invoke(this, damageable);
            damageable.TakeDamage(this, IgnoreInvincibility);
            if (DisableAfterHit)
                enabled = false;
        }
    }

    #endregion
}
