using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(RunController))]
public class AttackController : MonoBehaviour
{
    #region Private Properties

    /// <summary>
    /// A instance for the animator
    /// </summary>
    [SerializeField]
    private Animator animator;

    /// <summary>
    /// The amount of time the attack will lasts
    /// </summary>
    [SerializeField]
    [Tooltip("The amount of time the attack will lasts in seconds")]
    private float AttackTime = 1;

    /// <summary>
    /// The Last time the attack was effetuated
    /// </summary>
    private float LastAttackTime;

    #endregion

    public BoolEvent OnAttackEvent;

    [Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    #region Unity Events

    public void FixedUpdate()
    {
        // Cancel attack state
        if (Time.time >= LastAttackTime + AttackTime)
        {
            // Set proper animation
            animator.SetBool("Attack", false);

            OnAttackEvent.Invoke(false);
        }
    }

    #endregion

    public void Attack()
    {
        // Save attack time
        LastAttackTime = Time.time;

        // Set proper animation
        animator.SetBool("Attack", true);

        OnAttackEvent.Invoke(true);
    }
}
