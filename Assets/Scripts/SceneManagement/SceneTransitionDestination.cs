using UnityEngine;
using UnityEngine.Events;

public class SceneTransitionDestination : MonoBehaviour
{
    #region Types

    /// <summary>
    /// Tags for all destination points
    /// </summary>
    public enum DestinationTag
    {
        A, B, C, D, E, F, G,
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// The destination tag for this point
    /// </summary>
    public DestinationTag destinationTag;    // This matches the tag chosen on the TransitionPoint that this is the destination for.

    [Tooltip("This is the gameobject that has transitioned.  For example, the player.")]
    public GameObject transitioningGameObject;

    [Tooltip("Fired when the target reach the destination.")]
    public UnityEvent OnReachDestination;

    #endregion
}
