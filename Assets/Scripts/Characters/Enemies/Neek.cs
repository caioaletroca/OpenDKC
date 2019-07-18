using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Neek : Enemy
{
    #region Public Properties

    public Collider2D BaseCollider;

    public Collider2D KillCollider;

    public Collider2D DeathCollider;

    /// <summary>
    /// The direction for the death jump effect
    /// </summary>
    public Vector2 DeathDirection = new Vector2(0, 200);

    #endregion

    #region Private Properties

    /// <summary>
    /// A instance for the <see cref="Rigidbody2D"/> for the game Object
    /// </summary>
    private Rigidbody2D mRigidBody2D;

    #endregion 

    #region Unity Methods

    private void Awake()
    {
        mRigidBody2D = GetComponent<Rigidbody2D>();
    }

    #endregion

    private void Update()
    {
        
    }

    public override void TakeDamage()
    {
        base.TakeDamage();
        
        // Disable Collisions
        BaseCollider.enabled = false;

        // Mark state as dead
        Dead = true;

        // Add little jump
        mRigidBody2D.AddForce(DeathDirection);
    }
}
