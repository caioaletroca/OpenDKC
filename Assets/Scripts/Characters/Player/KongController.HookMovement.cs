using UnityEngine;

public partial class KongController
{
    #region Public Methods

    /// <summary>
    /// Handles the collision on a hook
    /// </summary>
    /// <param name="collision"></param>
    public void OnHookTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hook")
        {
            var hookComponent = collision.gameObject.GetComponentInParent<Hook>();
            if(hookComponent.CanHook) {
                // Reset the jump action so the player needs to jump again to
                // get off the hook
                Jump = false;
                
                Hook = true;
                SetParent(hookComponent.gameObject);
            }
        }
    }

    #endregion
}
