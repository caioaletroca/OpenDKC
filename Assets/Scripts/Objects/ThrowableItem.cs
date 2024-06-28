using UnityEngine;

/// <summary>
/// Component for any pickable and throwable item
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Damageable))]
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
    public GameObject BreakVFX;

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

    #endregion

    #region Unity Events

    protected void Awake() {
        mRigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();

        mRigidBody2D.bodyType = RigidbodyType2D.Kinematic;
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
    }

    public void PerformDrop() {
        Picked = false;

        // Clean up parent
        transform.parent = null;
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

    public void OnHitGround() {
        if(Throwed) {
            PerformBreak();
        }
    }

    #endregion
}