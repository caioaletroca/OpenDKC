using System.Collections;
using UnityEngine;

/// <summary>
/// A screen fader effect manager for the application
/// </summary>
public class ScreenFader : MonoBehaviour
{
    #region Singleton

    /// <summary>
    /// Private access for the singleton instance
    /// </summary>
    protected static ScreenFader mInstance;

    /// <summary>
    /// A singleton instance for the <see cref="ScreenFader"/>
    /// </summary>
    public static ScreenFader Instance
    {
        get
        {
            // Return if instantiated
            if (mInstance != null)
                return mInstance;

            // Find object in the scene
            mInstance = FindObjectOfType<ScreenFader>();
            if (mInstance != null)
                return mInstance;

            // TODO: Add ScreenFader prefab

            return mInstance;
        }
    }

    #endregion

    #region Types

    /// <summary>
    /// The types allowed for fading effects
    /// </summary>
    public enum FadeType { Black, Loading, GameOver }

    #endregion

    #region Public Properties

    /// <summary>
    /// The Black fade effect
    /// </summary>
    public CanvasGroup FaderCanvasGroup;

    /// <summary>
    /// The duration for the fade transition
    /// </summary>
    [Tooltip("The duration in seconds for the fade transition.")]
    public float FadeDuration = 0.5f;

    /// <summary>
    /// A flag that represents if the manager is currently fading the screen
    /// </summary>
    [HideInInspector]
    public bool IsFading;

    #endregion

    #region Unity Methods

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Set the alpha value for the fade canvas
    /// </summary>
    /// <param name="alpha">The new alpha value</param>
    public static void SetAlpha(float alpha) => Instance.FaderCanvasGroup.alpha = alpha;

    /// <summary>
    /// Fade the current scene in
    /// </summary>
    /// <returns></returns>
    public static IEnumerator FadeSceneIn()
    {
        var canvasGroup = Instance.FaderCanvasGroup;

        // Start fading effect
        yield return Instance.StartCoroutine(Instance.Fade(0f, canvasGroup));

        // Disable fader after the effect is complete
        canvasGroup.gameObject.SetActive(false);
    }

    /// <summary>
    /// Fade the current scene out
    /// </summary>
    /// <param name="fadeType">The type to fade</param>
    /// <returns></returns>
    public static IEnumerator FadeSceneOut(FadeType fadeType = FadeType.Black)
    {
        CanvasGroup canvasGroup;
        switch(fadeType)
        {
            case FadeType.Black:
                canvasGroup = Instance.FaderCanvasGroup;
                break;
            default:
                canvasGroup = Instance.FaderCanvasGroup;
                break;
        }

        // Set the fade effect active
        canvasGroup.gameObject.SetActive(true);

        // Start the fading effect
        yield return Instance.StartCoroutine(Instance.Fade(1f, canvasGroup));
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Fades the screen using a final alpha value
    /// </summary>
    /// <param name="finalAlpha">The final alpha value</param>
    /// <param name="canvasGroup">The canvas to fade</param>
    /// <returns></returns>
    protected IEnumerator Fade(float finalAlpha, CanvasGroup canvasGroup)
    {
        IsFading = true;
        canvasGroup.blocksRaycasts = true;
        float fadeSpeed = Mathf.Abs(canvasGroup.alpha - finalAlpha) / FadeDuration;
        while (!Mathf.Approximately(canvasGroup.alpha, finalAlpha))
        {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, finalAlpha, fadeSpeed * Time.deltaTime);
            yield return null;
        }
        canvasGroup.alpha = finalAlpha;
        IsFading = false;
        canvasGroup.blocksRaycasts = false;
    }

    #endregion
}
