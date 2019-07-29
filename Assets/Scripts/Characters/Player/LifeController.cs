using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handles the Life system for the player controller
/// </summary>
public class LifeController : MonoBehaviour, IDataPersister
{
    #region Types

    /// <summary>
    /// Default event type for the <see cref="LifeController"/>
    /// </summary>
    [Serializable]
    public class LifeEvent : UnityEvent<LifeController> { }

    #endregion

    #region Public Properties

    /// <summary>
    /// The start value for the counter
    /// </summary>
    public int StartValue = 4;
    
    /// <summary>
    /// The current life value for the player
    /// </summary>
    [HideInInspector]
    public int LifeCount
    {
        get => mLifeCount;
        set
        {
            if(mLifeCount != value)
            {
                mLifeCount = value;
                OnLifeCountChanged?.Invoke(this);
            }
        }
    }

    /// <summary>
    /// Fires when the life is loaded from persistence
    /// </summary>
    public LifeEvent OnLifeCountLoaded;

    /// <summary>
    /// Fires when the life value changes
    /// </summary>
    public LifeEvent OnLifeCountChanged;

    /// <summary>
    /// Fires when the life value reaches zero
    /// </summary>
    public LifeEvent OnLifeCountNegative;

    /// <summary>
    /// Instance for persistence configuration
    /// </summary>
    [HideInInspector]
    public DataSettings dataSettings;

    #endregion

    #region Private Properties

    /// <summary>
    /// Internal value for the life count
    /// </summary>
    private int mLifeCount;

    #endregion

    #region Unity Methods

    private void Awake() => LifeCount = StartValue;

    private void OnEnable() => PersistentDataManager.RegisterPersister(this);

    private void OnDisable() => PersistentDataManager.UnregisterPersister(this);

    #endregion

    #region Data Persistence Methods

    public DataSettings GetDataSettings() => dataSettings;

    public void SetDataSettings(string dataTag, DataSettings.PersistenceType persistenceType)
    {
        dataSettings.dataTag = dataTag;
        dataSettings.persistenceType = persistenceType;
    }

    public Data SaveData() => new Data<int>(LifeCount);

    public void LoadData(Data data)
    {
        // Load data
        LifeCount = ((Data<int>)data).value;

        // Fire event
        //OnBananaLoaded?.Invoke(this);
    }

    #endregion
}
