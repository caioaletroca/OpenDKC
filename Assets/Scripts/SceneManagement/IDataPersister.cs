using System;

public interface IDataPersister
{
    DataSettings GetDataSettings();

    void SetDataSettings(string dataTag, DataSettings.PersistenceType persistenceType);

    Data SaveData();

    void LoadData(Data data);
}

public class DataSettings
{
    #region Types

    public enum PersistenceType
    {
        NotPersist,
        ReadOnly,
        WriteOnly,
        ReadWrite,
    }

    #endregion

    #region Public Properties

    public string dataTag = Guid.NewGuid().ToString();

    public PersistenceType persistenceType = PersistenceType.ReadWrite;

    #endregion

    #region Public Methods

    public override string ToString() => $"{dataTag} {persistenceType.ToString()}";

    #endregion
}

public class Data { }


public class Data<T> : Data
{
    public T value;

    public Data(T value)
    {
        this.value = value;
    }
}

public class Data<T0, T1> : Data
{
    public T0 value0;
    public T1 value1;

    public Data(T0 value0, T1 value1)
    {
        this.value0 = value0;
        this.value1 = value1;
    }
}