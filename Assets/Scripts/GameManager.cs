using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Public Properties

    /// <summary>
    /// A singleton instance access
    /// </summary>
    [HideInInspector]
    public static GameManager Instance = null;

    public GameObject PlayerObject;

    public GameObject RespawnPoint;

    #endregion

    #region Private Methods

    [HideInInspector]
    public bool GameEnded = false;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        // Do not destroy Game Manager when loading
        DontDestroyOnLoad(gameObject);

        // Start the game
        Initialize();
    }

    #endregion

    #region Private Methods

    public void Initialize()
    {
        var player = Instantiate(PlayerObject, RespawnPoint.transform.position, RespawnPoint.transform.rotation);

        // Set new camera target
        CinemachineManager.Instance.m_Follow = player.transform;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        GameEnded = false;
    }

    public void Respawn()
    {
        var player = Instantiate(PlayerObject, RespawnPoint.transform.position, RespawnPoint.transform.rotation);

        // Set new camera target
        CinemachineManager.Instance.m_Follow = player.transform;

        GameEnded = false;
    }

    public void EndGame()
    {
        if(!GameEnded)
        {
            GameEnded = true;

            Respawn();
        }
    }

    #endregion
}
