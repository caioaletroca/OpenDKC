using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the global game persistence data
/// </summary>
public class PersistentDataManager : MonoBehaviour
{
    #region Singleton

    /// <summary>
    /// Internal singleton access
    /// </summary>
    protected static PersistentDataManager mInstance;

    /// <summary>
    /// A singleton access for this manager
    /// </summary>
    public static PersistentDataManager Instance
    {
        get
        {
            if (mInstance != null)
                return mInstance;

            mInstance = FindObjectOfType<PersistentDataManager>();

            if (mInstance != null)
                return mInstance;

            Create();
            return mInstance;
        }
    }

    /// <summary>
    /// Creates a new instance on the scene
    /// </summary>
    /// <returns></returns>
    public static PersistentDataManager Create()
    {
        var dataManagerGameObject = new GameObject("PersistentDataManager");
        DontDestroyOnLoad(dataManagerGameObject);
        mInstance = dataManagerGameObject.AddComponent<PersistentDataManager>();
        return mInstance;
    }

    #endregion

    #region Private Properties

    /// <summary>
    /// A storage for all the registered persisters
    /// </summary>
    protected HashSet<IDataPersister> mDataPersisters = new HashSet<IDataPersister>();

    /// <summary>
    /// Stores the data from persistence
    /// </summary>
    protected Dictionary<string, Data> mStore = new Dictionary<string, Data>();

    /// <summary>
    /// A action method to schedule the actions for the next update
    /// </summary>
    protected event Action schedule = null;

    /// <summary>
    /// A flag that represents if this instance of the manager needs to unregister all persisters before be destroyed
    /// </summary>
    protected static bool Quitting = false;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        if (Instance != this)
            Destroy(gameObject);
    }

    private void Update()
    {
        if(schedule != null)
        {
            schedule();
            schedule = null;
        }
    }

    private void OnDestroy()
    {
        if (mInstance == this)
            Quitting = true;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Registers a new persister on the manager
    /// </summary>
    /// <param name="persister">The new persister</param>
    public static void RegisterPersister(IDataPersister persister)
    {
        var dp = persister.GetDataSettings();
        if (!string.IsNullOrEmpty(dp.dataTag))
            Instance.Register(persister);
    }

    /// <summary>
    /// Unregister a new persister on the manager
    /// </summary>
    /// <param name="persister"></param>
    public static void UnregisterPersister(IDataPersister persister)
    {
        if (!Quitting)
            Instance.Unregister(persister);
    }

    /// <summary>
    /// Clear all the data on the persisters
    /// </summary>
    public static void ClearPersisters() => Instance.mDataPersisters.Clear();

    /// <summary>
    /// Saves all the data from persisters
    /// </summary>
    public static void SaveAllData() => Instance.SaveAllDataInternal();

    /// <summary>
    /// Loads all the data to the persisters
    /// </summary>
    public static void LoadAllData() => Instance.LoadAllDataInternal();

    #endregion

    #region Private Methods

    /// <summary>
    /// Register a persister on the manager system
    /// </summary>
    /// <param name="persister"></param>
    protected void Register(IDataPersister persister) => schedule += () => mDataPersisters.Add(persister);

    /// <summary>
    /// Unregister a persister on the manager system
    /// </summary>
    /// <param name="persister"></param>
    protected void Unregister(IDataPersister persister) => schedule += () => mDataPersisters.Remove(persister);

    /// <summary>
    /// Saves all data from persister internally
    /// </summary>
    protected void SaveAllDataInternal()
    {
        foreach (var dp in mDataPersisters)
            Save(dp);
    }

    /// <summary>
    /// Loads all data to the persister internally
    /// </summary>
    protected void LoadAllDataInternal()
    {
        foreach (var dp in mDataPersisters)
            Load(dp);
    }

    /// <summary>
    /// Saves data from a single persister
    /// </summary>
    /// <param name="dp"></param>
    protected void Save(IDataPersister dp)
    {
        var dataSettings = dp.GetDataSettings();
        if (dataSettings.persistenceType == DataSettings.PersistenceType.ReadOnly ||
            dataSettings.persistenceType == DataSettings.PersistenceType.NotPersist)
            return;
        if (!string.IsNullOrEmpty(dataSettings.dataTag))
            mStore[dataSettings.dataTag] = dp.SaveData();
    }

    /// <summary>
    /// Loads data to a single persister
    /// </summary>
    /// <param name="dp"></param>
    protected void Load(IDataPersister dp)
    {
        var dataSettings = dp.GetDataSettings();
        if (dataSettings.persistenceType == DataSettings.PersistenceType.WriteOnly ||
            dataSettings.persistenceType == DataSettings.PersistenceType.NotPersist)
            return;
        if (!string.IsNullOrEmpty(dataSettings.dataTag))
            if (mStore.ContainsKey(dataSettings.dataTag))
                dp.LoadData(mStore[dataSettings.dataTag]);
    }

    #endregion
}
