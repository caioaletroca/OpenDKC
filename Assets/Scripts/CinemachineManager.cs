using Cinemachine;
using UnityEngine;

public class CinemachineManager : MonoBehaviour
{
    #region Public Properties

    /// <summary>
    /// A singleton access for cinemachine camera
    /// </summary>
    public static CinemachineVirtualCamera Instance = null;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        if (Instance == null)
            Instance = GetComponent<CinemachineVirtualCamera>();
    }

    #endregion
}
