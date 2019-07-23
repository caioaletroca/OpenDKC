using UnityEngine;

public partial class KongController
{
    #region Public Properties

    /// <summary>
    /// The object the kong stickes to when on spawn state
    /// </summary>
    [HideInInspector]
    public GameObject SpawnGameObject;

    #endregion

    #region Public Methods

    /// <summary>
    /// Set the player to the spawn state
    /// </summary>
    /// <param name="state"></param>
    /// <param name="gameObject"></param>
    public void SetSpawnState(bool state, GameObject gameObject = null)
    {
        Spawn = state;
        SpawnGameObject = gameObject;
    }

    #endregion
}
