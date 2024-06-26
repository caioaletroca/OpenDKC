using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
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

    /// <summary>
    /// VFX Sound played when collected
    /// </summary>
    [Tooltip("VFX Sound played when collected.")]
    public string VFXName;

    /// <summary>
    /// The interval in seconds of each VFX spawn interval
    /// </summary>
    [Tooltip("The interval in seconds of each VFX spawn interval.")]
    public float SpawnVFXInterval = 0.08f;

    #endregion

    #region Private Methods

    /// <summary>
    /// Sprite Renderer instance
    /// </summary>
    private SpriteRenderer spriteRenderer;

    /// <summary>
    /// Animator instance
    /// </summary>
    private Animator animator;

    /// <summary>
    /// Box Collider 2D instance
    /// </summary>
    private BoxCollider2D boxCollider2D;

    #endregion

    #region Unity Methods

    protected void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

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

        // Disable all properties of object and wait for all spawn
        PerformCollected();

        StartCoroutine(BananaCollectedCoroutine(bananaUI, position));        
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Disable all object interactions
    /// </summary>
    private void PerformCollected() {
        spriteRenderer.enabled = false;
        animator.enabled = false;
        boxCollider2D.enabled = false;
    }

    private IEnumerator BananaCollectedCoroutine(BananaUI bananaUI, Vector3 position) {
        // Spawn sound effect
        VFXController.Instance.Trigger(VFXName, position, 0, false, null);

        for(int i = 0; i < Amount; i++) {
            // Spawn the prefab
            Instantiate(BananaIcon, position, transform.rotation, bananaUI.Canvas.transform);

            yield return new WaitForSeconds(SpawnVFXInterval);
        }

        // Destroy it self
        Destroy(gameObject);
    }

    #endregion
}
