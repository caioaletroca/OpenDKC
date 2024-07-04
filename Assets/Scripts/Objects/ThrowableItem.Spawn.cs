using UnityEngine;

/// <summary>
/// Implements spawnable options for throwable items
/// </summary>
public class ThrowableItemSpawn : MonoBehaviour {
    #region Public Properties

    /// <summary>
    /// Spawned object after this item is break
    /// </summary>
    [Tooltip("Spawned object after this item is break.")]
    public GameObject Spawn;

    /// <summary>
    /// Force applied on Y axis when the item is spawned
    /// </summary>
    [Tooltip("Force applied on Y axis when the item is spawned.")]
    public float Force = 10;

    #endregion

    #region Events Methods

    public void OnBreak() {
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