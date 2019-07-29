using System.Collections.Generic;
using TMPro;
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
    public CanvasGroup BananaUICanvas;

    /// <summary>
    /// Instance for the counter text
    /// </summary>
    public TextMeshProUGUI BananaCounterText;

    /// <summary>
    /// Instance for the Banana end point
    /// </summary>
    public Transform BananaEndPoint;

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

    private float VisibilityTimer;

    #endregion

    #region Unity Events

    private void Start()
    {
        // Register event on player
        KongController.Instance.BananaController.OnBananaCountChanged.AddListener(OnBananaCollected);
        KongController.Instance.BananaController.OnBananaLoaded.AddListener((e) =>
        {
            BananaCounterText.text = e.BananaCount.ToString();
        });

        if (AlwaysShow) ShowUI();
        else DisableUI();
    }

    private void Update()
    {
        if (BananaUICanvas.alpha == 0 || BananaIncome.Count != 0)
            return;

        if (BananaIncome.Count == 0)
        {
            if(VisibilityTimer == 0)
                VisibilityTimer = InactivityTime;

            VisibilityTimer -= Time.deltaTime;
        }

        if(VisibilityTimer <= 0)
        {
            DisableUI();
        }
    }

    #endregion

    #region Events Methods

    public void OnBananaCollected(BananaController bananaController)
    {
        // Add to the stack
        BananaIncome.Push(true);
        VisibilityTimer = 0;

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
        BananaCounterText.text = bananaController.BananaCount.ToString();

        // Remove from the stack
        BananaIncome.Pop();
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Shows the banana UI
    /// </summary>
    public void ShowUI() => BananaUICanvas.alpha = 1;

    /// <summary>
    /// Hides the banana UI
    /// </summary>
    public void DisableUI()
    {
        if (!AlwaysShow)
            BananaUICanvas.alpha = 0;
    }

    
    #endregion
}
