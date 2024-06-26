using UnityEngine;

/// <summary>
/// Component for any pickable and throwable item
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
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

    #endregion

    #region Unity Events

    protected void Awake() {
        mRigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    #endregion

    #region Public Methods

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

    #endregion
}