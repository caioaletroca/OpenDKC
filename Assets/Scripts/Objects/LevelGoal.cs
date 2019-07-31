using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the end level goal
/// </summary>
public class LevelGoal : MonoBehaviour
{
    #region Public Properties

    /// <summary>
    /// The sprite renderer for the prize place holder
    /// </summary>
    [Tooltip("The sprite renderer for the prize place holder.")]
    public SpriteRenderer PrizeHolder;

    /// <summary>
    /// The height the player needs to jump to be successful
    /// </summary>
    [Tooltip("The height the player needs to jump to be successful.")]
    public float SuccessfulHeight = 1;

    /// <summary>
    /// The amount of type every prize item swaps
    /// </summary>
    [Tooltip("The amount of type every prize item swaps.")]
    public float PrizeTime;

    /// <summary>
    /// The current index for the prize
    /// </summary>
    [Tooltip("Defines the current active prize.")]
    public int CurrentPrize;

    /// <summary>
    /// The list of all prizes in the level goal
    /// </summary>
    [SerializeField]
    public List<GameObject> Prizes;

    #endregion

    #region Private Properties

    /// <summary>
    /// The timer for the prize swap
    /// </summary>
    private float PrizeTimer;

    #endregion

    #region Unity Methods

    private void Start()
    {
        PrizeTimer = PrizeTime;

        // Set the sprite
        SetPrizeSprite(CurrentPrize);
    }

    private void Update()
    {
        UpdatePrizeTimer();
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the successful line
        var width = 3;
        var pointA = transform.TransformPoint(new Vector2(0, SuccessfulHeight));
        var pointB = transform.TransformPoint(new Vector2(-width, SuccessfulHeight));

        // Draw the line
        Gizmos.color = Color.green;
        Gizmos.DrawLine(pointA, pointB);
    }

    #endregion

    #region Event Methods

    /// <summary>
    /// Fires when the player hits the target
    /// </summary>
    /// <param name="collision"></param>
    public void OnPlayerHitGoal(Collider2D collision)
    {
        // Get the player controller
        var kongController = collision.gameObject.GetComponent<KongController>();

        // Check if the player is in the air
        if(kongController.Grounded == false && kongController.VerticalSpeed < 0)
        {
            // Check if player was successful
            if(kongController.HeightJump >= SuccessfulHeight)
            {

            }
        }
    }

    #endregion

    #region Update Methods

    public void UpdatePrizeTimer()
    {
        // Decremente the timer
        PrizeTimer -= Time.deltaTime;

        // If the timer explodes
        if (PrizeTimer < 0)
        {
            // Get the next prize index
            CurrentPrize = GetNextPrize(CurrentPrize);

            // Set the sprite
            SetPrizeSprite(CurrentPrize);    

            // Reset the timer
            PrizeTimer = PrizeTime;
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Sets the sprite place holder with the index
    /// </summary>
    /// <param name="index"></param>
    protected void SetPrizeSprite(int index)
    {
        // Set the next prize sprite
        PrizeHolder.sprite = Prizes[CurrentPrize].GetComponent<SpriteRenderer>().sprite;
    }

    /// <summary>
    /// Get the next prize index and overlap the list
    /// </summary>
    /// <param name="currentPrize">The current prize</param>
    /// <returns></returns>
    protected int GetNextPrize(int currentPrize)
    {
        var next = ++currentPrize;

        if (next >= Prizes.Count)
            next = 0;

        return next;
    }

    #endregion
}
