using UnityEngine;

/// <summary>
/// Renders a decade int renderer
/// </summary>
public class DecadeIntImageRenderer : MonoBehaviour
{
    #region Public Properties

    /// <summary>
    /// Instance for the decade place render
    /// </summary>
    [Tooltip("Instance for the decade place render.")]
    public UnitIntImageRenderer DecadePlace;

    /// <summary>
    /// Instance for the unit place render
    /// </summary>
    [Tooltip("Instance for the unit place render.")]
    public UnitIntImageRenderer UnitPlace;

    /// <summary>
    /// The current numeric value
    /// </summary>
    public int Value
    {
        get => CurrentValue;
        set
        {
            // Test if out of range
            if (value > 99)
            {
                Debug.LogError("No sprite defined for this number.");
                return;
            }

            // Check if value changed
            if (value != CurrentValue)
            {
                CurrentValue = value;

                // Renders new value
                Render();
            }
        }
    }

    /// <summary>
    /// The current value
    /// </summary>
    [Tooltip("The current value displayed.")]
    public int CurrentValue;

    #endregion

    #region Unity Methods

    public void Update() => Render();

    #endregion

    #region Private Methods

    /// <summary>
    /// Renders the new value
    /// </summary>
    protected void Render()
    {
        // Check for out of range
        if (CurrentValue > 99)
        {
            Debug.LogError("Sprite for this number not defined.");
            return;
        }

        // Set to renders
        DecadePlace.Value = CurrentValue / 10;
        UnitPlace.Value = CurrentValue % 10;
    }

    #endregion
}
