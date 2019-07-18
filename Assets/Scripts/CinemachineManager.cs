using Cinemachine;
using UnityEngine;

public class CinemachineManager : MonoBehaviour
{
    #region Public Properties

    /// <summary>
    /// A singleton access for cinemachine camera
    /// </summary>
    [HideInInspector]
    public static CinemachineVirtualCamera Instance = null;

    #endregion

    #region Unity Methods

    public void Awake()
    {
        //if (Instance == null)
            Instance = GetComponent<CinemachineVirtualCamera>();
    }

    #endregion
}
