using UnityEngine;

public class Hook : MonoBehaviour
{
    #region Public Properties

    [HideInInspector]
    public bool CanHook = true;

    #endregion

    #region Events

    public void OnEnterHook()
    {
        CanHook = false;
    }

    public void OnExitHook()
    {
        CanHook = true;
    }

    #endregion
}
