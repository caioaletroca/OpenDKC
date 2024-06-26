using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the Banana UI
/// </summary>
public class BananaUI : MonoBehaviour
{
    #region Public Properties

    /// <summary>
    /// Instance for the UI canvas
    /// </summary>
    public CanvasGroup Canvas;

    /// <summary>
    /// Instance for the counter text
    /// </summary>
    public DecadeIntImageRenderer Counter;

    /// <summary>
    /// Instance for the Banana end point
    /// </summary>
    public Transform EndPoint;

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
    /// Internal stored value for the timer
    /// </summary>
    private float VisibilityTimer;

    #endregion

    #region Unity Events

    private void Start()
    {
        // Register event on player
        KongController.Instance.BananaController.OnBananaCountChanged.AddListener(OnBananaCollected);
        KongController.Instance.BananaController.OnBananaLoaded.AddListener((e) =>
        {
            Counter.Value = e.BananaCount;
        });

        if (AlwaysShow) ShowUI();
        else DisableUI();
    }

    private void Update()
    {
        if (Canvas.alpha == 0)
            return;

        // Decrement timer
        VisibilityTimer -= Time.deltaTime;

        if (VisibilityTimer <= 0)
        {
            // Tries to disable the UI
            DisableUI();
        }
    }

    #endregion

    #region Events Methods

    /// <summary>
    /// Fired when a banana is collected
    /// </summary>
    /// <param name="bananaController"></param>
    public void OnBananaCollected(BananaController bananaController)
    {
        // Sets the timer
        VisibilityTimer = InactivityTime * 10f;

        // Shows the UI
        ShowUI();
    }

    /// <summary>
    /// Fired when a banana arrives the destination in the Banana UI
    /// </summary>
    /// <param name="bananaController"></param>
    public void OnBananaArrived(BananaController bananaController)
    {
        // Slowly updates the count, one by one
        Counter.Value++;

        // Sets the timer
        VisibilityTimer = InactivityTime;
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
