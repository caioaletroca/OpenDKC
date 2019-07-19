using UnityEngine;

public partial class KongController
{
    #region Public Properties

    [HideInInspector]
    public GameObject HookGameObject;

    #endregion

    #region Public Methods

    public void Teleport(Vector2 Position)
    {
        transform.position = Position;
        mRigidBody2D.velocity = Vector2.zero;
    }

    #endregion
}
