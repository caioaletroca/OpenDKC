using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TransitionPoint : MonoBehaviour
{
    #region Types

    public enum TransitionType { DifferentScene, DifferentNonGameplayScene, SameScene }

    public enum TransitionWhen { ExternalCall, InteractPressed, OnTriggerEnter }

    #endregion

    #region Public Properties

    [Tooltip("This is the gameobject that will transition.  For example, the player.")]
    public GameObject TransitionGameObject;

    [Tooltip("Whether the transition will be within this scene, to a different zone or a non-gameplay scene.")]
    public TransitionType transitionType;

    public string newSceneName;

    [Tooltip("What should trigger the transition to start.")]
    public TransitionWhen transitionWhen;

    #endregion
}
