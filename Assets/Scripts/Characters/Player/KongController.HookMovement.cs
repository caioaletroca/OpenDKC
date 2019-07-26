using UnityEngine;

public partial class KongController
{
    #region Public Methods

    public void SetLocalPosition(Vector3 Position) => transform.localPosition = Position;

    /// <summary>
    /// Handles the collision on a hook
    /// </summary>
    /// <param name="collision"></param>
    public void OnHookTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hook")
        {
            Hook = true;
            SetParent(collision.gameObject);
        }
    }

    /// <summary>
    /// Handles the collision exit on a hook
    /// </summary>
    /// <param name="collision"></param>
    public void OnHookTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hook")
        {
            Hook = false;
        }
    }

    #endregion
}
