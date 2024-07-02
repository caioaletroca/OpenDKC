using UnityEngine;

/// <summary>
/// Component for any pickable and throwable item
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Damager))]
[RequireComponent(typeof(Damageable))]
[RequireComponent(typeof(ProximityActivator))]
public class ThrowableItem : MonoBehaviour {
    #region State Variables

    /// <summary>
    /// Flag that represents if this object has been picked
    /// </summary>
    [HideInInspector]
    public bool Picked {
        get => animator.GetBool("Picked");
        set => animator.SetBool("Picked", value);
    }

    /// <summary>
    /// Flag that represents if this object has been picked
    /// </summary>
    [HideInInspector]
    public bool Throwed {
        get => animator.GetBool("Throwed");
        set => animator.SetBool("Throwed", value);
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// VFX spawned when this item breaks
    /// </summary>
    [Tooltip("VFX spawned when this item breaks.")]
    public GameObject BreakVFX;

    /// <summary>
    /// The ground layer
    /// </summary>
    [Tooltip("Ground layer for collision detection.")]
    public LayerMask GroundLayer;

    /// <summary>
    /// Flag that represents if the item should take a hit when hit the ground on throw
    /// </summary>
    [Tooltip("Flag that represents if the item should take a hit when hit the ground on throw.")]
    public bool Fragile;

    public Vector2 Offset;

    #endregion

    #region Protected Properties

    /// <summary>
    /// Rigidbody 2D instance
    /// </summary>
    protected Rigidbody2D mRigidBody2D;

    /// <summary>
    /// Animator instance
    /// </summary>
    protected Animator animator;

    /// <summary>
    /// Damageable instance
    /// </summary>
    protected Damageable damageable;

    /// <summary>
    /// Proximity Activator instance
    /// </summary>
    protected ProximityActivator proximityActivator;

    #endregion

    #region Unity Events

    protected void Awake() {
        mRigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
        proximityActivator = GetComponent<ProximityActivator>();

        mRigidBody2D.bodyType = RigidbodyType2D.Kinematic;
    }

    protected void OnDestroy() {
        if(Picked) {
            var kongController = transform.parent.GetComponent<KongController>();
            if(kongController) {
                kongController.OnThrowableDestroy();
            }
        }
    }

    #endregion

    #region Public Methods

    public void SetParent(GameObject gameObject) {
        transform.parent = gameObject.transform;
    }

    /// <summary>
    /// Set a new local position using <see cref="Rigidbody2D"/>
    /// </summary>
    /// <param name="position">The new position</param>
    public void SetLocalPosition(Vector3 position)
    {
        // Preserve Z position
        var newPosition = new Vector3(position.x, position.y, transform.position.z);

        // Set local position
        if (transform.parent != null)
            mRigidBody2D.MovePosition(transform.parent.TransformPoint(newPosition));
        else
            mRigidBody2D.MovePosition(transform.TransformPoint(newPosition));
    }

    public void PerformPick() {
        Picked = true;

        // Disable proximity
        proximityActivator.SoftActive = false;
    }

    public void PerformDrop() {
        Picked = false;

        // Clean up parent
        transform.parent = null;
        proximityActivator.SoftActive = true;

        SnapToGround();
    }

    public void PerformThrow(Vector2 force) {
        Picked = false;
        Throwed = true;

        mRigidBody2D.bodyType = RigidbodyType2D.Dynamic;
        
        mRigidBody2D.AddForce(force, ForceMode2D.Impulse);

        // Clean up parent
        transform.parent = null;

        mRigidBody2D.gravityScale = 1;
    }

    public void PerformBreak() {
        VFXController.Instance.Trigger("BarrelBreakXF", transform.position);

        Destroy(gameObject);
    }

    #endregion

    #region Events Methods

    public void OnTakeDamage(Damager damager, Damageable damageable) {
        var itemDamager = GetComponent<Damager>();

        // Get enemy damageable instance
        var enemyDamageable = damager.GetComponentInParent<Damageable>();

        // Force damage into enemy
        enemyDamageable.TakeDamage(itemDamager);
    }

    public void OnHitGround() {
        if(Throwed && Fragile) {
            damageable.TakeDamage(1);
        }
    }

    #endregion

    #region Protected Methods

    /// <summary>
    /// Snaps the object right above the ground
    /// </summary>
    protected void SnapToGround() {
        // Calculate point on the ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 10, GroundLayer);
        if(hit.collider != null) {
            transform.position = new Vector2(transform.position.x, hit.point.y + Offset.y);
        }
    }

    #endregion
}