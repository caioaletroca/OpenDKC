using UnityEngine;
using static GameControls;

/// <summary>
/// Stores the input system
/// </summary>
public class InputController : MonoBehaviour
{
    #region Singleton

    /// <summary>
    /// A singleton instance for the Player Input
    /// </summary>
    [HideInInspector]
    public static InputController Instance;

    #endregion

    #region Public Properties

    [HideInInspector]
    public KongMapActions KongMap;

    [HideInInspector]
    public float HorizontalValue;

    [HideInInspector]
    public float VerticalValue;

    [HideInInspector]
    public bool Pause;

    [HideInInspector]
    public bool Jump;

    [HideInInspector]
    public bool Attack;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);

        // Register events
        KongMap = new GameControls().KongMap;
        KongMap.Jump.performed += e => Jump = true;
        KongMap.Jump.canceled += e => Jump = false;
        KongMap.Attack.performed += e => Attack = true;
        KongMap.Attack.canceled += e => Attack = false;
        KongMap.HorizontalAxis.performed += e => HorizontalValue = e.ReadValue<float>();
        KongMap.HorizontalAxis.canceled += e => HorizontalValue = 0;
        KongMap.VerticalAxis.performed += e => VerticalValue = e.ReadValue<float>();
        KongMap.VerticalAxis.canceled += e => VerticalValue = 0;
        KongMap.Pause.started += e => Pause = !Pause;
    }

    private void OnEnable() => KongMap.Enable();

    private void OnDisable() => KongMap.Disable();

    #endregion

    #region Public Methods

    public static void Enable() => Instance.KongMap.Enable();

    public static void Disable() => Instance.KongMap.Disable();

    #endregion
}
