using UnityEngine;

public partial class KongController
{
    #region Public Methods

    public void SetLocalPosition(Vector2 Position)
    {
        transform.localPosition = Position;
        mRigidBody2D.velocity = Vector2.zero;
    }

    #endregion
}
