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
    public void RestartButton()
    {
        // First, unpauses the game
        SceneController.Unpause();

        // Then, restart the current scene
        SceneController.Restart();
    }

    /// <summary>
    /// Quits the game
    /// </summary>
    public void QuitButton() => SceneController.Quit();

    #endregion
}
