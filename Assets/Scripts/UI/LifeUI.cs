using UnityEngine;

/// <summary>
/// Controls the Life UI for the game
/// </summary>
public class LifeUI : MonoBehaviour
{
    #region Public Properties

    /// <summary>
    /// Instance for the owner canvas
    /// </summary>
    public CanvasGroup Canvas;

    /// <summary>
    /// Instance for the counter text
    /// </summary>
    public UnitIntImageRenderer Counter;

    /// <summary>
    /// The Inactivity time to hide the UI
    /// </summary>
    [Tooltip("The Inactivity time to hide the UI.")]
    public int InactivityTime = 2;

    /// <summary>
    /// A flag that represents if the UI should stay always visible
    /// </summary>
    [Tooltip("If enabled, the UI will be always visible.")]
    public bool AlwaysShow;

    #endregion

    #region Private Properties

    /// <summary>
    /// The value stored for the timer
    /// </summary>
    private float VisibilityTimer;

    #endregion

    #region Unity Methods

    protected void Start()
    {
        // Register events
        KongController.Instance.LifeController.OnLifeCountChanged.AddListener(OnLifeCountChanged);
        KongController.Instance.LifeController.OnLifeCountLoaded.AddListener(OnLifeCountLoaded);

        if (AlwaysShow) ShowUI();
        else DisableUI();

        // Update value
        OnLifeCountLoaded(KongController.Instance.LifeController);
    }

    protected void Update()
    {
        // Do not update timer if invisible
        if (Canvas.alpha == 0)
            return;
                
        // Decrement the timer
        VisibilityTimer -= Time.deltaTime;

        if (VisibilityTimer <= 0)
        {
            // Tries to disable the UI
            DisableUI();
        }
    }

    #endregion

    #region Events Methods

    public void OnLifeCountChanged(LifeController lifeController)
    {
        // Shows the UI
        ShowUI();

        // Sets timer
        VisibilityTimer = InactivityTime;

        // Update the value
        Counter.Value = lifeController.LifeCount;

        // If enabled, never register disable action
        if (AlwaysShow)
            return;
    }

    /// <summary>
    /// Fired when the life counter loads from memory
    /// </summary>
    public void OnLifeCountLoaded(LifeController lifeController)
    {
        Counter.Value = lifeController.LifeCount;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Shows the banana UI
    /// </summary>
    public void ShowUI() => Canvas.alpha = 1;

    /// <summary>
    /// Hides the banana UI
    /// </summary>
    public void DisableUI()
    {
        if (!AlwaysShow)
            Canvas.alpha = 0;
    }

    #endregion
}
