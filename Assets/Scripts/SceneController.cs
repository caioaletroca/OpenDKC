using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    #region Public Properties

    /// <summary>
    /// A singleton instance access
    /// </summary>
    [HideInInspector]
    public static SceneController Instance = null;

    [HideInInspector]
    public Scene CurrentScene;

    [HideInInspector]
    public SceneTransitionDestination.DestinationTag RestartDestinationTag;

    public SceneTransitionDestination InitialSceneTransitionDestination;

    /// <summary>
    /// A flag that represents if the controller is transitioning scenes
    /// </summary>
    [HideInInspector]
    public bool IsTransitioning;

    #endregion

    #region Private Methods

    [HideInInspector]
    public bool GameEnded = false;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        // Do not destroy Game Manager when loading
        DontDestroyOnLoad(gameObject);

        // Start the game
        Initialize();
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Restarts the current scene
    /// </summary>
    public static void Restart()
    {
        Instance.StartCoroutine(Instance.Transition(Instance.CurrentScene.name, Instance.RestartDestinationTag));
    }

    #endregion

    #region Private Methods

    protected IEnumerator Transition(string newSceneName, SceneTransitionDestination.DestinationTag destinationTag)
    {
        IsTransitioning = true;

        // Disables inputs during transition
        PlayerInput.Instance.KongMap.Disable();

        // Start fading effect
        yield return StartCoroutine(ScreenFader.FadeSceneOut(ScreenFader.FadeType.Black));

        // Loads scene
        yield return SceneManager.LoadSceneAsync(newSceneName);

        // Find the spawn point
        var entrance = FindDestination(destinationTag);

        // Set the entering game object the position
        SetEnteringGameObjectLocation(entrance);

        // Set the new scene
        SetCurrentScene(entrance);

        // Fire reach event
        if (entrance != null)
            entrance.OnReachDestination.Invoke();

        // Start the fade in effect
        yield return StartCoroutine(ScreenFader.FadeSceneIn());

        // Re-enables inputs
        PlayerInput.Instance.KongMap.Enable();

        IsTransitioning = false;
    }

    protected SceneTransitionDestination FindDestination(SceneTransitionDestination.DestinationTag destinationTag)
    {
        // Find entrances on the already loaded scene
        var entrances = FindObjectsOfType<SceneTransitionDestination>();
        foreach (var entrance in entrances)
        {
            if (entrance.destinationTag == destinationTag)
                return entrance;
        }
        Debug.LogWarning($"No entrance was found with the {destinationTag} tag.");
        return null;
    }

    protected void SetEnteringGameObjectLocation(SceneTransitionDestination entrance)
    {
        if(entrance == null)
        {
            Debug.LogWarning("Entering Transform's location has not been set.");
            return;
        }

        // Set the entering game object the new position
        var entranceGameObject = entrance.transitioningGameObject;
        entranceGameObject.transform.position = entrance.transform.position;
        entranceGameObject.transform.rotation = entrance.transform.rotation;
    }

    protected void SetCurrentScene(SceneTransitionDestination entrance)
    {
        CurrentScene = entrance.gameObject.scene;
        RestartDestinationTag = entrance.destinationTag;
    }

    protected void Initialize()
    {
        if(InitialSceneTransitionDestination != null)
        {
            SetEnteringGameObjectLocation(InitialSceneTransitionDestination);
            ScreenFader.SetAlpha(1f);
            StartCoroutine(ScreenFader.FadeSceneIn());
            InitialSceneTransitionDestination.OnReachDestination.Invoke();
        }
        else
        {
            CurrentScene = SceneManager.GetActiveScene();
            RestartDestinationTag = SceneTransitionDestination.DestinationTag.A;
        }
    }

    #endregion
}
