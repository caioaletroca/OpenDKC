using UnityEngine;

/// <summary>
/// Implements spawnable options for throwable items
/// </summary>
[RequireComponent(typeof(ThrowableItem))]
public class Chest : MonoBehaviour {
    #region State Variables

    /// <summary>
    /// Flag that represents if the chest is spawning
    /// </summary>
    public bool Spawned;

    #endregion

    #region Public Properties

    /// <summary>
    /// Amount of health the chest has
    /// </summary>
    [Tooltip("Amount of health the chest has.")]
    public int Health = 2;

    /// <summary>
    /// Spawned object after this item is fully break
    /// </summary>
    [Tooltip("Spawned object after this item is fully break.")]
    public GameObject Spawn;

    /// <summary>
    /// Force applied on Y axis when the item is spawned
    /// </summary>
    [Tooltip("Force applied on Y axis when the item is spawned.")]
    public float Force = 8;

    #endregion

    #region Private Properties

    /// <summary>
    /// Throwable Item instance
    /// </summary>
    private ThrowableItem throwableItem;

    #endregion

    #region Unity Events

    protected void Awake() {
        throwableItem = GetComponent<ThrowableItem>();
    }

    #endregion

    #region Events Methods

    public void OnBreak() {
        // Recursevily spawn another chest with less health
        // until the internal item can be spawned
        if(Health > 1) {
            SpawnNext();
            return;
        }

        SpawnItem();
    }

    public void OnHitGround() {
        if(Spawned == true) {
            Spawned = false;
            throwableItem.Idle = true;
        }

        // Do nothing if the collider is triggered, but the item has not been thrown
        if(!throwableItem.Throwed) {
            return;
        }

        // Since the chest is meant to be picked again after throw
        // Reset state variables when grounded
        throwableItem.Throwed = false;
        throwableItem.Idle = true;
    }

    #endregion

    #region Protected Methods

    /// <summary>
    /// Spawn next chest iteration
    /// </summary>
    protected void SpawnNext() {
        // Spawn itself
        // TODO: Fix Z position
        var gameObject = Instantiate(this.gameObject, new Vector3(transform.position.x, transform.position.y, 1), transform.rotation);

        // Get components
        var rb = gameObject.GetComponent<Rigidbody2D>();
        var throwableItem = gameObject.GetComponent<ThrowableItem>();
        var chest = gameObject.GetComponent<Chest>();

        // Mark as not idle to avoid the player pick the chest mid air
        // while the spawning jump is happening
        chest.Spawned = true;
        throwableItem.Idle = false;

        // Add spawn force
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddForce(new Vector2(0, Force), ForceMode2D.Impulse);

        // Decrease chest health
        chest.Health = Health - 1;
    }

    /// <summary>
    /// Spawn item inside the chest
    /// </summary>
    protected void SpawnItem() {
        var gameObject = Instantiate(Spawn, transform.position, transform.rotation);

        // Add physics components to make the spawn jump works
        var rb = (Rigidbody2D)gameObject.AddComponent(typeof(Rigidbody2D));
        var col = (CircleCollider2D)gameObject.AddComponent(typeof(CircleCollider2D));

        // Set configuration
        col.radius = 0.2f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.gravityScale = 3;
        rb.AddForce(new Vector2(0, Force), ForceMode2D.Impulse);
    }

    #endregion
}