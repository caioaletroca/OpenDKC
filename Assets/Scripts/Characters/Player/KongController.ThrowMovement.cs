using UnityEngine;

public partial class KongController {
    
    #region Public Properties

    [HideInInspector]
    public ThrowableItem ItemHolded;

    #endregion
    
    #region Public Methods

    /// <summary>
    /// Snaps the holded item position into the offset
    /// </summary>
    public void PerformItemSnap() {
        ItemHolded.SetLocalPosition(ThrowSettings.SnapOffset);
    }

    public void PerformItemPickup(ThrowableItem item) {
        ItemHolded = item;

        ItemHolded.PerformPick();
        PerformItemSnap();
    }

    public void PerformItemDrop() {
        ItemHolded.PerformDrop();

        ItemHolded = null;
    }

    #endregion

    #region Event Methods

    public void OnThrowableTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Throwable") {
            // Only allow hold if Attack or Running is activated
            if(Attack || Run) {
                Debug.Log("Done");
                Hold = true;
                collision.gameObject.transform.parent = gameObject.transform;

                var throwableItem = collision.gameObject.GetComponent<ThrowableItem>();
                PerformItemPickup(throwableItem);
            }
        }
    }

    #endregion
}