using UnityEngine;

public class BananaCollectXF : MonoBehaviour
{
    #region Public Properties

    /// <summary>
    /// The fly speed which the banana will fly to the counter UI
    /// </summary>
    [Tooltip("The fly speed which the banana will fly to the counter UI.")]
    public float MaximumSpeed = 5;

    /// <summary>
    /// The acceleration for the fly movement
    /// </summary>
    [Tooltip("The acceleration for the movement.")]
    public float Acceleration = 1;

    /// <summary>
    /// The proximity to the target to finish the movement
    /// </summary>
    [Tooltip("The proximity to the target to finish the movement.")]
    public float TargetProximity = 50;

    #endregion

    #region Private Properties

    /// <summary>
    /// The target where the banana XF is flying
    /// </summary>
    private Vector2 TargetPoint;

    /// <summary>
    /// The internal velocity for the movement
    /// </summary>
    private float velocity = 1;

    #endregion

    #region Unity Methods

    private void Start()
    {
        // Get the Target point
        var bananaUI = FindObjectOfType<BananaUI>();
        if (bananaUI == null)
            Debug.LogError("Could not find the Banana UI.");

        TargetPoint = bananaUI.BananaEndPoint.position;
    }

    private void FixedUpdate()
    {
        // Calculate the direction
        var direction = TargetPoint - (Vector2)transform.position;

        // Calculate the velocity
        velocity += Acceleration * Time.fixedDeltaTime;
        velocity = Mathf.Clamp(velocity, 0, MaximumSpeed);

        // Move the object
        transform.position = (Vector2)transform.position + velocity * Time.fixedDeltaTime * direction.normalized;
        
        // Check if arrived
        if (Vector2.Distance(transform.position, TargetPoint) < 50)
            OnBananaArrived();
    }

    #endregion

    #region Event Methods

    public void OnBananaArrived()
    {
        // Notify the counter
        var bananaUI = FindObjectOfType<BananaUI>();
        bananaUI.OnBananaArrived();

        // Destroy the banana
        Destroy(gameObject);
    }

    #endregion
}
