﻿using System.Collections;
using UnityEngine;

/// <summary>
/// A screen fader effect manager for the application
/// </summary>
public class ScreenFader : MonoBehaviour
{
    #region Singleton

    /// <summary>
    /// A singleton instance for the <see cref="ScreenFader"/>
    /// </summary>
    public static ScreenFader Instance;

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
        if (Instance == null)
            Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    #endregion

    #region Public Methods

    public static void SetAlpha(float alpha)
    {
        Instance.FaderCanvasGroup.alpha = alpha;
    }

    public static IEnumerator FadeSceneIn()
    {
        var canvasGroup = Instance.FaderCanvasGroup;

        // Start fading effect
        yield return Instance.StartCoroutine(Instance.Fade(0f, canvasGroup));

        // Disable fader after the effect is complete
        canvasGroup.gameObject.SetActive(false);
    }

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