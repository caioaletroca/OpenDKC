using UnityEngine;

public class BananaItem : MonoBehaviour
{
    #region Public Properties

    /// <summary>
    /// The amount of bananas this item values
    /// </summary>
    [Tooltip("The amount of bananas this item values.")]
    public int Amount = 1;

    /// <summary>
    /// The prefab to spawn the icon
    /// </summary>
    [Tooltip("The prefab to spawn the screen icon.")]
    public GameObject BananaIcon;

    #endregion

    #region Private Methods

    /// <summary>
    /// A instance for the animator
    /// </summary>
    private Animator animator;

    /// <summary>
    /// A instance for the collider
    /// </summary>
    private Collider2D colliders;

    #endregion

    #region Unity Events

    private void Awake() => animator = GetComponent<Animator>();

    #endregion

    #region Event Methods

    /// <summary>
    /// Fired when the banana is collected
    /// </summary>
    public void OnBananaCollected()
    {
        // Get the UI instance
        var bananaUI = FindObjectOfType<BananaUI>();
        var position = Camera.main.WorldToScreenPoint(transform.position);

        // Spawn the prefab
        Instantiate(BananaIcon, position, transform.rotation, bananaUI.Canvas.transform);

        // Destroy it self
        Destroy(gameObject);
    }

    #endregion
}
