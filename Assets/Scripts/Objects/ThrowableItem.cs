using UnityEngine;
using UnityEngine.Events;

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
    /// Flag that represents if this object is idle
    /// </summary>
    // [HideInInspector]
    public bool Idle = true;

    /// <summary>
    /// Flag that represents if this object has been picked
    /// </summary>
    [HideInInspector]
    public bool Picked {
        get => animator.GetBool("Picked");
        set {
            if(value == true) {
                Idle = false;
            }

            animator.SetBool("Picked", value);
        }
    }

    /// <summary>
    /// Flag that represents if this object has been picked
    /// </summary>
    [HideInInspector]
    public bool Throwed {
        get => animator.GetBool("Throwed");
        set {
            if(value == true) {
                Idle = false;
            }

            animator.SetBool("Throwed", value);
        }
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Flag that represents if the item should take damage when hit the ground on throw
    /// </summary>
    [Tooltip("Flag that represents if the item should take damage when hit the ground on throw.")]
    public bool Fragile;

    public Vector2 Offset;

    [Space(10)]

    /// <summary>
    /// Event called when the item is thrown.
    /// </summary>
    [Tooltip("Event called when the item is thrown.")]
    public UnityEvent OnThrow;

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

        // Disable damageable. The item should only take damage when picked or throwed.
        damageable.enabled = false;
    }

    protected void OnDestroy() {
        if(!transform.parent) {
            return;
        }
        
        var kongController = transform.parent.GetComponentInParent<KongController>();
        
        if(kongController) {
            kongController.OnThrowableDestroy();
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

        // Turn into kinematic to enable player to handle it
        mRigidBody2D.bodyType = RigidbodyType2D.Kinematic;

        // Disable proximity
        proximityActivator.SoftActive = false;

        // Enable damage
        damageable.enabled = true;
    }

    public void PerformDrop() {
        Idle = true;
        Picked = false;

        // Clean up parent
        transform.parent = null;
        proximityActivator.SoftActive = true;

        // Disable damage
        damageable.enabled = false;

        SnapToGround();
    }

    public void PerformThrow(Vector2 force) {
        Throwed = true;

        mRigidBody2D.bodyType = RigidbodyType2D.Dynamic;
        mRigidBody2D.AddForce(force, ForceMode2D.Impulse);
        mRigidBody2D.gravityScale = 1;

        // Clean up parent
        transform.parent = null;

        // Enable damage
        damageable.enabled = true;

        // Call event
        OnThrow.Invoke();
    }

    public void PerformBreak() {
        VFXController.Instance.Trigger("BarrelBreakVFX", transform.position);

        Destroy(gameObject);
    }

    #endregion

    #region Events Methods

    public void OnTakeDamage(Damager damager, Damageable damageable) {
        // Check if the damage taken came from an enemy
        if(damager) {
            var throwableItemDamager = GetComponent<Damager>();

            // Get enemy damageable instance
            var enemyDamageable = damager.GetComponentInParent<Damageable>();

            // Force damage into enemy
            enemyDamageable.TakeDamage(throwableItemDamager);
        }
    }

    public void OnHitGround() {
        // Do nothing if the collider is triggered, but the item has not been thrown
        if(!Throwed) {
            return;
        }

        if(Fragile) {
            damageable.TakeDamage(1);

            // If the item is broken, play the break SFX
            if(damageable.Health == 0) {
                VFXController.Instance.Trigger("BarrelBreakSFX", transform.position);
            }

            return;
        }

        Picked = false;
    }

    public void OnHitWall() {
        // Do nothing if the collider is triggered, but the item has not been thrown
        if(!Throwed) {
            return;
        }

        damageable.TakeDamage(1);

        // If the item is broken, play the break SFX
        if(damageable.Health == 0) {
            VFXController.Instance.Trigger("BarrelBreakSFX", transform.position);
        }
    }

    #endregion

    #region Protected Methods

    /// <summary>
    /// Snaps the object right above the ground
    /// </summary>
    protected void SnapToGround() {
        // Calculate point on the ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 10, LayerMask.GetMask("Ground"));
        if(hit.collider != null) {
            transform.position = new Vector2(transform.position.x, hit.point.y + Offset.y);
        }
    }

    #endregion
}