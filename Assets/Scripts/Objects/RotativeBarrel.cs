using UnityEngine;

/// <summary>
/// Controls the rotative barrel
/// </summary>
[RequireComponent(typeof(Animator))]
public class RotativeBarrel : BlastBarrel
{
    #region State Variables

    /// <summary>
    /// Defines if the player is inside the barrel
    /// </summary>
    [HideInInspector]
    public bool Full
    {
        get => animator.GetBool("Full");
        set => animator.SetBool("Full", value);
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Instance for the number icon
    /// </summary>
    [Tooltip("Instance for the Number Icon.")]
    public UnitIntSpriteRenderer NumberIcon;

    /// <summary>
    /// The amount of time the barrel will auto shoot
    /// </summary>
    [Tooltip("The amount of time the barrel will auto shoot.")]
    public int BlastTime = 5;

    #endregion

    #region Private Properties

    /// <summary>
    /// Instance for the animator
    /// </summary>
    private Animator animator;

    /// <summary>
    /// The timer to store the blast maximum time
    /// </summary>
    private float BlastTimer;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        NumberIcon.Value = BlastTime;
    }

    private void FixedUpdate()
    {
        // Only use timer when full
        if (Full)
        {
            // Calculate time
            BlastTimer -= Time.fixedDeltaTime;
            NumberIcon.Value = (int)Mathf.Round(BlastTimer);

            // If explodes, auto fire
            if(BlastTimer < 0)
                ForceFire();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Full = true;
            BlastTimer = BlastTime;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Full = false;
            NumberIcon.Value = BlastTime;
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Force the player out of the barrel
    /// </summary>
    public void ForceFire()
    {
        // Find kong child
        var kongController = transform.GetComponentInChildren<KongController>();
        if(kongController != null)
            kongController.Blast = true;
    }

    #endregion
}
