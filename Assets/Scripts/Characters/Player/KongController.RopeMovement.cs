using UnityEngine;

public partial class KongController {
    #region Private Properties

    #endregion

    #region Public Methods

    public void PerformRopeGrap() {
        mRigidBody2D.gravityScale = 0;
        mRigidBody2D.velocity = Vector2.zero;
    }

    #endregion

    #region Event Methods

    private void OnRopeTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Rope") {
            Rope = true;

            SetParent(collision.gameObject);
        }
    }

    #endregion

    #region Private Methods

    

    #endregion
}