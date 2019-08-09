using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Constrols a object that is activated whenever a certain object is in range
/// </summary>
public class ProximityActivator : MonoBehaviour
{
    #region Public Properties

    /// <summary>
    /// The minimum distance to activate the enemy
    /// </summary>
    [Tooltip("The minimum distance to activate the enemy.")]
    public float Distance = 10;

    /// <summary>
    /// The player layer whose will activate the enemy
    /// </summary>
    [Tooltip("The player layer whose will activate the enemy.")]
    public LayerMask Layer;

    /// <summary>
    /// A flag that represents if the object should return after re-enabled
    /// </summary>
    [Tooltip("If enabled, the object will return to its spawn starting position after re-activated.")]
    public bool ReturnSpawn = false;

    /// <summary>
    /// A flag that represents if the object should be destroyed if deactivated
    /// </summary>
    [Tooltip("If enabled, the object will be destroyed after deactivated.")]
    public bool DestroyOnDeactivate = false;

    /// <summary>
    /// Fires when the object is activated
    /// </summary>
    public UnityEvent OnActivateEvent;

    /// <summary>
    /// Fires when the object is deactivate
    /// </summary>
    public UnityEvent OnDeactivateEvent;

    /// <summary>
    /// A flag that represents if the system is activated
    /// </summary>
    public bool Active
    {
        get => mActive;
        set
        {
            // Do not update if not fully started
            if (!WaitForStart)
                return;

            // Test for changed value
            if(value != mActive)
            {
                mActive = value;

                if (mActive) FireActivate();
                else FireDeactivate();
            }
        }
    }

    #endregion

    #region Private Properties

    /// <summary>
    /// Internal value for the <see cref="Active"/> variable
    /// </summary>
    protected bool mActive = false;

    /// <summary>
    /// The starting position for respawn options
    /// </summary>
    protected Vector2 StartingPosition;

    /// <summary>
    /// The starting scale for respawn options
    /// </summary>
    protected Vector2 StartingScale;

    /// <summary>
    /// A flag to wait for the start method
    /// </summary>
    protected bool WaitForStart = false;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        // Get the starting position
        StartingPosition = transform.position;
        StartingScale = transform.localScale;
    }

    private void Start()
    {
        WaitForStart = true;

        // First start procedure
        mActive = CheckProximity();
        if (mActive) FireActivate();
        else FireDeactivate();
    }

    private void Update()
    {
        Active = CheckProximity();
    }

    private void OnEnable() => Active = true;

    private void OnDisable() => Active = false;

    private void OnDrawGizmosSelected()
    {
        // Draw activator radius
        Gizmos.color = Color.yellow;
        Handles.Label(transform.position + new Vector3(0, Distance), "Activator Radius");
        Gizmos.DrawWireSphere(transform.position, Distance);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Checks for the activation proximity conditions
    /// </summary>
    protected bool CheckProximity()
    {
        var hit = Physics2D.OverlapCircle(transform.position, Distance, Layer);

        // Update state variable
        return hit != null;
    }

    /// <summary>
    /// Fires the <see cref="OnActivateEvent"/>
    /// </summary>
    protected void FireActivate()
    {
        // Fire event
        OnActivateEvent?.Invoke();
    }

    /// <summary>
    /// Fires the <see cref="OnDeactivateEvent"/>
    /// </summary>
    protected void FireDeactivate()
    {
        // Fire event
        OnDeactivateEvent?.Invoke();

        // Return to spawn point if enabled
        if (ReturnSpawn)
        {
            transform.position = StartingPosition;
            transform.localScale = StartingScale;
        }

        // If enabled, destroy
        if (DestroyOnDeactivate)
            Destroy(gameObject);
    }

    #endregion
}