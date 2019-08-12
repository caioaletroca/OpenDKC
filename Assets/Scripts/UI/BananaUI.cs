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

    private Stack<bool> BananaIncome = new Stack<bool>();

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
        if (Canvas.alpha == 0 || BananaIncome.Count != 0)
            return;

        if (BananaIncome.Count == 0)
        {
            // Decrement timer
            VisibilityTimer -= Time.deltaTime;

            if (VisibilityTimer <= 0)
            {
                // Tries to disable the UI
                DisableUI();
            }
        }
    }

    #endregion

    #region Events Methods

    public void OnBananaCollected(BananaController bananaController)
    {
        // Add to the stack
        BananaIncome.Push(true);

        // Sets the timer
        VisibilityTimer = InactivityTime;

        // Shows the UI
        ShowUI();

        // If enabled, never register disable action
        if (AlwaysShow)
            return;
    }

    /// <summary>
    /// Fired when a banana arrives the destination in the Banana UI
    /// </summary>
    public void OnBananaArrived(BananaController bananaController)
    {
        Counter.Value = bananaController.BananaCount;

        // Remove from the stack
        BananaIncome.Pop();
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
