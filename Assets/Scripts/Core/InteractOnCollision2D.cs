using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handles collision interactions on game objects
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class InteractOnCollision2D : Interact
{
    #region Public Properties

    /// <summary>
    /// The event fired when the interaction happens
    /// </summary>
    public UnityEvent OnCollision;

    /// <summary>
    /// The event fired when the interaction happens with parameters
    /// </summary>
    public InteractCollision2DEvent OnCollisionParameter;

    #endregion

    #region Unity Methods

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(Layers.Contains(collision.gameObject))
        {
            OnCollision?.Invoke();
            OnCollisionParameter?.Invoke(collision);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "InteractionTrigger", false);
    }

    #endregion
}
