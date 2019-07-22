using UnityEngine;

/// <summary>
/// Controls events on the Pause Menu UI
/// </summary>
public class PauseUI : MonoBehaviour
{
    #region Public Methods

    /// <summary>
    /// Resumes the current game
    /// </summary>
    public void ResumeButton() => SceneController.Unpause();

    /// <summary>
    /// Restarts the current scene
    /// </summary>
    public void RestartButton() => SceneController.Restart();

    #endregion
}
