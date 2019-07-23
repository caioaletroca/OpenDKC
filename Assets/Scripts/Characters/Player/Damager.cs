using UnityEngine;
using UnityEngine.Events;

public class Damager : MonoBehaviour
{
    #region Types

    /// <summary>
    /// Event for damaging hits for the <see cref="Damager"/> class
    /// </summary>
    public class DamageableEvent : UnityEvent<Damager, Damageable> { }

    /// <summary>
    /// Event for no damaging hits for the <see cref="Damager"/> class
    /// </summary>
    public class NonDamageableEvent : UnityEvent<Damager> { }

    #endregion

    #region Public Properties

    /// <summary>
    /// The total damage inflicted on enemies
    /// </summary>
    [Tooltip("The total damage inflicted on enemies.")]
    public int Damage = 1;

    /// <summary>
    /// 
    /// </summary>
    public Vector2 Offset = new Vector2(1.5f, 1f);

    public Vector2 Size = new Vector2(2.5f, 1f);

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

    #endregion

    #region Private Properties

    /// <summary>
    /// A flag that represents if the damager is activated or not
    /// </summary>
    protected bool CanDamage = false;

    protected ContactFilter2D mContactFilter;

    protected Collider2D[] mOverlapResults = new Collider2D[10];

    protected Collider2D mLastHit;

    #endregion

    #region Events

    /// <summary>
    /// Fired when damager makes a hit with damage
    /// </summary>
    public DamageableEvent OnDamageableHit;

    /// <summary>
    /// Fired when damager makes a hit without damage
    /// </summary>
    public NonDamageableEvent OnNonDamageableHit;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        mContactFilter.layerMask = HittableLayers;
        mContactFilter.useLayerMask = true;
        mContactFilter.useTriggers = CanHitTrigger;
    }

    private void FixedUpdate()
    {
        // Does nothing if its disabled
        if (!CanDamage)
            return;

        // Calculate bounds points
        var scale = transform.lossyScale;
        var scaledSize = Vector2.Scale(Size, scale);
        var facingOffset = Vector2.Scale(Offset, scale);

        var pointA = (Vector2)transform.position + facingOffset - scaledSize * 0.5f;
        var pointB = pointA + scaledSize;

        // Check for hits
        var hitCount = Physics2D.OverlapArea(pointA, pointB, mContactFilter, mOverlapResults);
        for (int i = 0; i < hitCount; i++)
        {
            // Get collider
            mLastHit = mOverlapResults[i];

            // Get the damageable component instance
            var damageable = mLastHit.GetComponent<Damageable>();

            if(damageable)
            {
                OnDamageableHit?.Invoke(this, damageable);
                damageable.TakeDamage(this, IgnoreInvincibility);
                if (DisableAfterHit)
                    Disable();
            }
            else
                OnNonDamageableHit?.Invoke(this);
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Enables the damager system
    /// </summary>
    public void Enable() => CanDamage = true;

    /// <summary>
    /// Disables the damager system
    /// </summary>
    public void Disable() => CanDamage = false;

    #endregion

    private void OnDrawGizmos()
    {
        // Calculate bounds points
        var scale = transform.lossyScale;
        var scaledSize = Vector2.Scale(Size, scale);
        var facingOffset = Vector2.Scale(Offset, scale);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + (Vector3)facingOffset, scaledSize);
    }
}
