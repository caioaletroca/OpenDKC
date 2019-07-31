﻿using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handles collision interactions on game objects
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class InteractOnTrigger2D : Interact
{
    #region Public Properties

    /// <summary>
    /// The event fired when the interaction happens
    /// </summary>
    public UnityEvent OnTrigger;

    /// <summary>
    /// The event fired when the interaction happens with parameters
    /// </summary>
    public Interact2DEvent OnTriggerParameter;

    #endregion

    #region Unity Methods

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (Layers.Contains(collider.gameObject))
        {
            OnTrigger?.Invoke();
            OnTriggerParameter?.Invoke(collider);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "InteractionTrigger", false);
    }

    #endregion
}
