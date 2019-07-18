using UnityEngine;

public class BaseController : MonoBehaviour
{
    #region Public Methods

    public virtual void HorinzontalAxis(Vector2 movement, bool run = false, bool crouch = false) { }

    public virtual void JumpButton() { }

    public virtual void AttackButton() { }

    #endregion
}
