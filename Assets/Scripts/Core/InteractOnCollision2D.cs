using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handles collision interactions on game objects
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class InteractOnCollision2D : MonoBehaviour
{
    #region Public Properties

    /// <summary>
    /// The layers allowed to interact with the game object
    /// </summary>
    [Tooltip("The layers allowed to interact with the game object.")]
    public LayerMask Layers;

    /// <summary>
    /// The event fired when the interaction happens
    /// </summary>
    public UnityEvent OnCollision;

    #endregion

    #region Unity Methods

    private void Reset()
    {
        Layers = LayerMask.NameToLayer("Everything");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(Layers.Contains(collision.gameObject))
        {
            OnCollision.Invoke();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "InteractionTrigger", false);
    }

    #endregion
}
