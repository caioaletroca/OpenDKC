using UnityEditor;

/// <summary>
/// A custom inspector editor for the <see cref="IDataPersister"/>
/// </summary>
public abstract class DataPersisterEditor : Editor
{
    #region Private Properties

    /// <summary>
    /// The data to show on inspector
    /// </summary>
    IDataPersister mDataPersister;

    #endregion

    #region Unity Methods

    private void OnEnable() => mDataPersister = (IDataPersister)target;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DataPersisterGUI(mDataPersister);
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Renders the custom inspector
    /// </summary>
    /// <param name="dataPersister"></param>
    public static void DataPersisterGUI(IDataPersister dataPersister)
    {
        var dataSettings = dataPersister.GetDataSettings();

        var persistenceType = (DataSettings.PersistenceType)EditorGUILayout.EnumPopup("Persistence Type", dataSettings.persistenceType);
        var dataTag = EditorGUILayout.TextField("Data Tag", dataSettings.dataTag);

        dataPersister.SetDataSettings(dataTag, persistenceType);
    }

    #endregion
}