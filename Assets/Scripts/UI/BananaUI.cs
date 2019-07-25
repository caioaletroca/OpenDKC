using System.Collections;
using System.Threading;
using System.Threading.Tasks;
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

    #region Unity Events

    private void Start()
    {
        // Register event on player
        KongController.Instance.BananaController.OnBananaCountChanged.AddListener(OnBananaCollected);

        if (AlwaysShow) ShowUI();
        else DisableUI();
    }

    #endregion

    #region Events Methods

    public void OnBananaCollected(BananaController bananaController)
    {
        // Shows the UI
        ShowUI();

        // If enabled, never register disable action
        if (AlwaysShow)
            return;

        // Check if needed to cancel previous task
        //if (DisableUITask != null && !DisableUITask.IsCompleted)
        //    CTS_DisableUIAsync.Cancel();

        // Runs async
        
    }

    /// <summary>
    /// Fired when a banana arrives the destination in the Banana UI
    /// </summary>
    public void OnBananaArrived()
    {
        BananaCounterText.text = KongController.Instance.BananaController.BananaCount.ToString();
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
