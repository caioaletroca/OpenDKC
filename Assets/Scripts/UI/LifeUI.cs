using TMPro;
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
    public TextMeshProUGUI CounterText;

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

    #region Unity Methods

    private void Start()
    {
        // Register events
        KongController.Instance.LifeController.OnLifeCountChanged.AddListener(OnLifeCountChanged);
        KongController.Instance.LifeController.OnLifeCountLoaded.AddListener(OnLifeCountLoaded);

        if (AlwaysShow) ShowUI();
        else DisableUI();

        // Update value
        OnLifeCountLoaded(KongController.Instance.LifeController);
    }

    #endregion

    #region Events Methods

    public void OnLifeCountChanged(LifeController lifeController)
    {
        // Shows the UI
        ShowUI();

        // Update the value
        CounterText.text = lifeController.LifeCount.ToString();

        // If enabled, never register disable action
        if (AlwaysShow)
            return;
    }

    /// <summary>
    /// Fired when the life counter loads from memory
    /// </summary>
    public void OnLifeCountLoaded(LifeController lifeController)
    {
        CounterText.text = lifeController.LifeCount.ToString();
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
