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

    /// <summary>
    /// Instance for the kong mapping
    /// </summary>
    [HideInInspector]
    public KongMapActions KongMap;

    /// <summary>
    /// Instance for the barrel mapping
    /// </summary>
    [HideInInspector]
    public BarrelMapActions BarrelMap;

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

    [HideInInspector]
    public bool SideButton;

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

        // Register events
        BarrelMap = new GameControls().BarrelMap;
        KongMap = new GameControls().KongMap;
        KongMap.Jump.performed += e => Jump = true;
        KongMap.Jump.canceled += e => Jump = false;
        KongMap.Attack.performed += e => Attack = true;
        KongMap.Attack.canceled += e => Attack = false;
        KongMap.HorizontalAxis.performed += e => HorizontalValue = e.ReadValue<float>();
        KongMap.HorizontalAxis.canceled += e => HorizontalValue = 0;
        KongMap.VerticalAxis.performed += e => VerticalValue = e.ReadValue<float>();
        KongMap.VerticalAxis.canceled += e => VerticalValue = 0;
        KongMap.Pause.performed += e => Pause = true;
        KongMap.Pause.canceled += e => Pause = false;
        KongMap.SideButtons.performed += e => SideButton = true;
        KongMap.SideButtons.canceled += e => SideButton = false;
    }

    private void OnEnable() => KongMap.Enable();

    private void OnDisable() => KongMap.Disable();

    #endregion

    #region Public Methods

    public static void Enable() => Instance.KongMap.Enable();

    public static void Disable() => Instance.KongMap.Disable();

    #endregion
}
