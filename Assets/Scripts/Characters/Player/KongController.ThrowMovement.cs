using UnityEngine;

public partial class KongController {
    
    #region Public Properties

    [HideInInspector]
    public ThrowableItem ItemHolded;

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
        var force = new Vector2(mFacingRight ? 1 : -1, 0);

        // Check for type of throw
        force *= VerticalValue > 0.5 ? ThrowSettings.UpThrowForce : ThrowSettings.NormalThrowForce;

        ItemHolded.PerformThrow(force);
        ItemHolded = null;
    }

    #endregion

    #region Event Methods

    public void OnThrowableTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Throwable") {
            // Only allow hold if Attack or Running is activated
            if(Attack || Run) {
                Hold = true;

                PerformItemPickup(collision.gameObject.GetComponent<ThrowableItem>());
            }
        }
    }

    #endregion
}