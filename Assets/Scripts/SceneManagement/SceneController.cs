using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the game and the scene
/// </summary>
public class SceneController : MonoBehaviour
{
    #region Singleton

    /// <summary>
    /// A singleton instance access
    /// </summary>
    [HideInInspector]
    public static SceneController Instance = null;

    #endregion

    #region Public Properties

    [HideInInspector]
    public Scene CurrentScene;

    [HideInInspector]
    public SceneTransitionDestination.DestinationTag RestartDestinationTag;

    /// <summary>
    /// The initial default starting point for this scene
    /// </summary>
    public SceneTransitionDestination InitialSceneTransitionDestination;

    /// <summary>
    /// A flag that represents if the controller is transitioning scenes
    /// </summary>
    [HideInInspector]
    public bool IsTransitioning;

    #endregion

    #region Private Properties

    /// <summary>
    /// A flag state variable to define if the game is paused or not
    /// </summary>
    [HideInInspector]
    public bool Paused = false;

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
    }

    private void Start()
    {
        // Register Bindings
        InputController.Instance.KongMap.Pause.started += Pause_started;

        // Start the game
        Initialize();
    }

    #endregion

    #region Events Methods

    /// <summary>
    /// Fired when the user press the pause button
    /// </summary>
    /// <param name="obj"></param>
    private void Pause_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!Instance.Paused) Pause();
        else Unpause();
    }

    private void OnGameOverEvent()
    {
        // Find the player
        var player = FindObjectOfType<KongController>();

        // Give the player new lifes
        player.LifeController.LifeCount = 4;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Pauses the current scene and shows the Pause UI
    /// </summary>
    public static void Pause()
    {
        // Disable all buttons
        InputController.Disable();

        // Only enables the unpause button
        InputController.Instance.KongMap.Pause.Enable();

        // Stops time
        Time.timeScale = 0;

        // Loads the pause UI menus
        SceneManager.LoadSceneAsync("UIMenus", LoadSceneMode.Additive);

        // Set internal variable
        Instance.Paused = true;
    }

    /// <summary>
    /// Unpauses the current scene
    /// </summary>
    public static void Unpause()
    {
        if (Time.timeScale > 0)
            return;
        
        // Re-enables the inputs
        InputController.Enable();
        
        // Call unpause async
        Instance.StartCoroutine(Instance.UnpauseCoroutine());
    }

    /// <summary>
    /// Restarts the current scene
    /// </summary>
    public static void Restart(bool resetHealth = true)
    {
        // Resets health state
        if (resetHealth && KongController.Instance != null)
            KongController.Instance.Damageable.Health = KongController.Instance.Damageable.StartingHealth;
        
        // Reloads the scene
        Instance.StartCoroutine(Instance.Transition(Instance.CurrentScene.name, Instance.RestartDestinationTag));
    }

    /// <summary>
    /// Restarts the current scene with a specified delay time
    /// </summary>
    /// <param name="delay"></param>
    public static void RestartDelay(float delay, bool resetHealth = true)
    {
        // Reloads the scene with delay
        Instance.StartCoroutine(CallWithDelay(delay, Restart, resetHealth));
    }

    /// <summary>
    /// Closes completely the game
    /// </summary>
    public static void Quit()
    {
        // Fade the screen
        ScreenFader.FadeSceneOut();

        // Close game
        Application.Quit();
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Initialize the entire game
    /// </summary>
    protected void Initialize()
    {
        if (InitialSceneTransitionDestination != null)
        {
            // Set entering place
            SetEnteringGameObjectLocation(InitialSceneTransitionDestination);
            SetCurrentScene(InitialSceneTransitionDestination);

            // Fade screen
            ScreenFader.SetAlpha(1f);

            // Make Player Invisible
            // TODO: Remove this
            //KongController.Instance.SetSpawnState(true, InitialSceneTransitionDestination.gameObject);

            // Fade the Screen
            StartCoroutine(ScreenFader.FadeSceneIn());

            // Start Spawn animation
            InitialSceneTransitionDestination.OnReachDestination.Invoke();
        }
        else
        {
            CurrentScene = SceneManager.GetActiveScene();
            RestartDestinationTag = SceneTransitionDestination.DestinationTag.A;
        }
    }

    /// <summary>
    /// Executes the unpause routine async
    /// </summary>
    /// <returns></returns>
    protected IEnumerator UnpauseCoroutine()
    {
        // Resets the time
        Time.timeScale = 1;

        // Unloads the pause UI menus
        SceneManager.UnloadSceneAsync("UIMenus");

        // Re-enables the inputs
        InputController.Enable();

        // We have to wait for a fixed update so the pause button state change, otherwise we can get in case were the update
        // of this script happen BEFORE the input is updated, leading to setting the game in pause once again
        yield return new WaitForFixedUpdate();
        yield return new WaitForEndOfFrame();

        // Sets the state variable
        Paused = false;
    }

    protected IEnumerator Transition(string newSceneName, SceneTransitionDestination.DestinationTag destinationTag)
    {
        IsTransitioning = true;

        PersistentDataManager.SaveAllData();

        // Disables inputs during transition
        InputController.Disable();

        // Start fading effect
        yield return StartCoroutine(ScreenFader.FadeSceneOut(ScreenFader.FadeType.Black));

        PersistentDataManager.ClearPersisters();

        // Loads scene
        yield return SceneManager.LoadSceneAsync(newSceneName);
        
        PersistentDataManager.LoadAllData();

        // Find the spawn point
        var entrance = FindDestination(destinationTag);

        // Set the entering game object the position
        SetEnteringGameObjectLocation(entrance);

        // Set the new scene
        SetCurrentScene(entrance);

        // Make Player Invisible
        // TODO: Remove this
        KongController.Instance.SetSpawnState(true, entrance.gameObject);

        // Fire reach event
        if (entrance != null)
            entrance.OnReachDestination.Invoke();

        // Start the fade in effect
        yield return StartCoroutine(ScreenFader.FadeSceneIn());

        // Re-register the input bindings
        InputController.Instance.KongMap.Pause.started += Pause_started;

        // Re-enables inputs
        InputController.Enable();

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

    /// <summary>
    /// Calls an action after the delay specified in seconds
    /// </summary>
    /// <param name="delay"></param>
    /// <param name="call"></param>
    /// <returns></returns>
    protected static IEnumerator CallWithDelay(float delay, Action call)
    {
        yield return new WaitForSeconds(delay);
        call();
    }

    /// <summary>
    /// Calls an action after the delay specified in seconds
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="delay"></param>
    /// <param name="call"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    protected static IEnumerator CallWithDelay<T>(float delay, Action<T> call, T parameter)
    {
        yield return new WaitForSeconds(delay);
        call(parameter);
    }

    #endregion
}
