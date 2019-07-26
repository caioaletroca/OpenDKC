using UnityEngine;

/// <summary>
/// Controls the neek enemy
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Neek : MonoBehaviour
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

    #region Public Properties

    /// <summary>
    /// A instance for the damager class
    /// </summary>
    public Damager Damager;

    /// <summary>
    /// The direction for the death jump effect
    /// </summary>
    [Tooltip("The force applied when the object dies.")]
    public Vector2 DeathJumpForce = new Vector2(0, 200);

    /// <summary>
    /// The amount of time to despawn the object after death
    /// </summary>
    [Tooltip("The amount of time to despawn the object after death.")]
    public float TimeToDespawn = 5;

    /// <summary>
    /// The persistence data configuration
    /// </summary>
    [HideInInspector]
    public DataSettings dataSettings = new DataSettings()
    {
        persistenceType = DataSettings.PersistenceType.NotPersist,
    };

    #endregion

    #region Private Properties

    /// <summary>
    /// A instance for the <see cref="Rigidbody2D"/> for the game Object
    /// </summary>
    private Rigidbody2D mRigidBody2D;

    /// <summary>
    /// A instance for the <see cref="Animator"/> for the game Object
    /// </summary>
    private Animator animator;

    /// <summary>
    /// A instance for all the <see cref="Collider2D"/> for the game Object
    /// </summary>
    private Collider2D[] colliders;

    #endregion 

    #region Unity Methods

    private void Awake()
    {
        mRigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        colliders = GetComponents<Collider2D>();

        // Enables damager
        Damager.Enable();
    }

    #endregion

    #region Event Methods

    /// <summary>
    /// Fired when the enemy dies
    /// </summary>
    /// <param name="damager"></param>
    /// <param name="damageable"></param>
    public void OnDieEvent(Damager damager, Damageable damageable)
    {
        // Set state variable
        Die = true;

        // Disables damager
        Damager.Disable();

        // Resets velocity
        mRigidBody2D.velocity = Vector2.zero;
        
        // Perform Death jump
        mRigidBody2D.AddForce(new Vector2(DeathJumpForce.x * damageable.DamageDirection.x, DeathJumpForce.y), ForceMode2D.Impulse);

        // Disables all colliders
        foreach (var collider in colliders)
            collider.enabled = false;

        // Despawn in time
        Destroy(gameObject, TimeToDespawn);
    }

    #endregion
}
