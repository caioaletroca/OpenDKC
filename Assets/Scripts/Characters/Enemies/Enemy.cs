using UnityEngine;

/// <summary>
/// Defines a base class for all enemies
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    #region State Variables

    /// <summary>
    /// Represents if the object has died
    /// </summary>
    [HideInInspector]
    public bool Die
    {
        get => animator.GetBool("Die");
        set => animator.SetBool("Die", value);
    }

    #endregion

    #region Private Properties

    /// <summary>
    /// A instance for the <see cref="Rigidbody2D"/> for the game Object
    /// </summary>
    protected Rigidbody2D mRigidBody2D;

    /// <summary>
    /// A instance for the <see cref="Animator"/> for the game Object
    /// </summary>
    protected Animator animator;

    /// <summary>
    /// A instance for all the <see cref="Collider2D"/> for the game Object
    /// </summary>
    protected Collider2D[] colliders;

    #endregion 

    #region Unity Methods

    protected void Awake()
    {
        mRigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        colliders = GetComponents<Collider2D>();
    }

    #endregion

    #region Event Methods

    /// <summary>
    /// Fired when the enemy dies
    /// </summary>
    /// <param name="damager"></param>
    /// <param name="damageable"></param>
    public virtual void OnDieEvent(Damager damager, Damageable damageable) { }

    #endregion

    #region Private Methods

    /// <summary>
    /// Flips the sprite for the object
    /// </summary>
    protected void Flip()
    {
        // Multiply the x local scale by -1
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    #endregion
}
