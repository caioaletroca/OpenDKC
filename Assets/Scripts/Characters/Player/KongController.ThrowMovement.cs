using UnityEngine;

public partial class KongController {
    
    #region Public Properties

    [HideInInspector]
    public ThrowableItem ItemHolded;

    #endregion

    #region Private Properties

    /// <summary>
    /// Stored velocity to perform a throw freeze in mid air
    /// </summary>
    private Vector2 StoredVelocity = Vector2.zero;

    #endregion
    
    #region Public Methods

    public void PerformItemPickup(ThrowableItem item) {
        ItemHolded = item;
        ItemHolded.SetParent(ThrowSettings.ThrowAnchor);
        ItemHolded.transform.localPosition = Vector2.zero;

        ItemHolded.PerformPick();
    }

    public void PerformItemDrop() {
        ItemHolded.PerformDrop();

        ItemHolded = null;
    }

    public void PerformItemThrow() {
        // Check throw direction
        var force = new Vector2(mFacingRight ? 1 : -1, 1);

        // Check for type of throw
        force *= VerticalValue > 0.5 ? ThrowSettings.UpThrowForce : ThrowSettings.NormalThrowForce;

        Debug.Log(VerticalValue > 0.5 ? "ThrowSettings.UpThrowForce" : "ThrowSettings.NormalThrowForce");

        ItemHolded.PerformThrow(force);
        ItemHolded = null;
    }

    /// <summary>
    /// Starts the freeze movement throw
    /// </summary>
    public void PerformFreezeThrow() {
        // TODO: Solve for mid rise jump to continue after throw
        StoredVelocity = mVelocity;

        DisableGravity();
        mRigidBody2D.velocity = Vector2.zero;
    }

    /// <summary>
    /// Restored back from freeze throw
    /// </summary>
    public void PerformUnfreezeThrow() {
        EnableGravity();

        // TODO: Find a way to preserve speed properly
        // mRigidBody2D.velocity = StoredVelocity;
    }

    #endregion

    #region Event Methods

    public void OnThrowableTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Throwable") {
            // Only allow hold if Attack or Running is activated
            if(Attack || Run) {
                // Check if throwable can be picked
                var throwableItem = collision.gameObject.GetComponent<ThrowableItem>();
                if(!throwableItem.Picked && !throwableItem.Throwed) {
                    Hold = true;

                    PerformItemPickup(collision.gameObject.GetComponent<ThrowableItem>());
                }
            }
        }
    }

    public void OnThrowableDestroy() {
        ItemHolded = null;

        Hold = false;
    }

    #endregion
}