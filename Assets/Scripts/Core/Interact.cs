using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Defines a base interactible class
/// </summary>
public class Interact : MonoBehaviour
{
    #region Types

    /// <summary>
    /// Event with parameters
    /// </summary>
    [Serializable]
    public class Interact2DEvent : UnityEvent<Collider2D> { }

    #endregion

    #region Public Properties

    /// <summary>
    /// The layers allowed to interact with the game object
    /// </summary>
    [Tooltip("The layers allowed to interact with the game object.")]
    public LayerMask Layers;

    #endregion

    #region Unity Methods

    private void Reset()
    {
        Layers = LayerMask.NameToLayer("Everything");
    }

    #endregion
}
