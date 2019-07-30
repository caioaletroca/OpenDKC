using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Renders a Unit int value
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class UnitIntSpriteRenderer : MonoBehaviour
{
    #region Public Properties

    /// <summary>
    /// A flag that represents if the component will always render
    /// </summary>
    [Tooltip("If enabled, the component will set the sprite on every update cycle.")]
    public bool AlwaysRender;

    /// <summary>
    /// The current numeric value
    /// </summary>
    //[Tooltip("The current displayed value.")]
    [SerializeField]
    public int Value
    {
        get => CurrentValue;
        set
        {
            // Test if out of range
            if (value > Numbers.Count)
            {
                Debug.LogError("No sprite defined for this number.");
                return;
            }

            // Check if value changed
            if(value != CurrentValue)
            {
                CurrentValue = value;

                // Renders new value
                Render();
            }
        }
    }

    /// <summary>
    /// The sprites used to represent numbers
    /// </summary>
    [SerializeField]
    public List<Sprite> Numbers;

    #endregion

    #region Private Properties

    /// <summary>
    /// Instance for the sprite renderer
    /// </summary>
    private SpriteRenderer spriteRenderer;

    /// <summary>
    /// The internal current value
    /// </summary>
    [SerializeField]
    private int CurrentValue;

    /// <summary>
    /// A flag that represents if there is a render request
    /// </summary>
    private bool renderRequest;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        renderRequest = true;
    }

    private void Update()
    {
        if (renderRequest || AlwaysRender)
            Render();
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Renders the new value
    /// </summary>
    protected void Render()
    {
        // Check for out of range
        if(CurrentValue > Numbers.Count)
        {
            Debug.LogError("Sprite for this number not defined.");
            return;
        }
        
        // Renders number
        spriteRenderer.sprite = Numbers[CurrentValue];

        // Disabel request
        renderRequest = false;
    }

    #endregion
}
