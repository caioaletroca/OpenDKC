using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handles the Banana system for the player controller
/// </summary>
public class BananaController : MonoBehaviour, IDataPersister
{
    #region Types

    /// <summary>
    /// Default banana controller events
    /// </summary>
    [Serializable]
    public class BananaEvent : UnityEvent<BananaController> { }

    #endregion

    #region Public Properties

    /// <summary>
    /// The current banana count value
    /// </summary>
    [HideInInspector]
    public int BananaCount
    {
        get => mBananaCount;
        set
        {
            if (mBananaCount != value)
            {
                mBananaCount = value;
                OnBananaCountChanged?.Invoke(this);
            }
        }
    }

    /// <summary>
    /// Settings for the data persister
    /// </summary>
    [HideInInspector]
    public DataSettings dataSettings;

    /// <summary>
    /// Fired when the banana value changes
    /// </summary>
    public BananaEvent OnBananaCountChanged;

    /// <summary>
    /// Fires when the banana value overflows one hundred
    /// </summary>
    public BananaEvent OnBananaOverflow;

    /// <summary>
    /// Fires when the banana value is loaded from persistence
    /// </summary>
    public BananaEvent OnBananaLoaded;

    #endregion

    #region Private Properties

    /// <summary>
    /// Internal value for the banana count
    /// </summary>
    protected int mBananaCount = 0;

    #endregion

    #region Unity Methods

    private void OnEnable() => PersistentDataManager.RegisterPersister(this);

    private void OnDisable() => PersistentDataManager.UnregisterPersister(this);

    #endregion

    #region Event Methods

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collision happens with a banana
        if (collision.gameObject.tag == "Banana")
        {
            var bananaItem = collision.gameObject.GetComponent<BananaItem>();
            if(bananaItem != null)
            {
                // Check if the value has overflow 100
                if (BananaCount + bananaItem.Amount > 100)
                {
                    BananaCount += bananaItem.Amount - 100;
                    OnBananaOverflow?.Invoke(this);
                }
                // Normal addition
                else
                    BananaCount += bananaItem.Amount;

                // Fire event on Item
                bananaItem.OnBananaCollected();
            }
        }
    }

    #endregion

    #region Data Persistence Methods

    public DataSettings GetDataSettings() => dataSettings;

    public void SetDataSettings(string dataTag, DataSettings.PersistenceType persistenceType)
    {
        dataSettings.dataTag = dataTag;
        dataSettings.persistenceType = persistenceType;
    }

    public Data SaveData() => new Data<int>(BananaCount);

    public void LoadData(Data data)
    {
        // Load data
        BananaCount = ((Data<int>)data).value;

        // Fire event
        OnBananaLoaded?.Invoke(this);
    }

    #endregion
}
