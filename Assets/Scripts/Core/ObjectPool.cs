using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles a pool of objects
/// </summary>
/// <typeparam name="TPool"></typeparam>
/// <typeparam name="TObject"></typeparam>
/// <typeparam name="TInfo"></typeparam>
public abstract class ObjectPool<TPool, TObject, TInfo> : ObjectPool<TPool, TObject>
    where TPool : ObjectPool<TPool, TObject, TInfo>
    where TObject : PoolObject<TPool, TObject, TInfo>, new()
{
    #region Unity Methods

    private void Start()
    {
        for (int i = 0; i < initialPoolCount; i++)
        {
            var newPoolObject = CreateNewPoolObject();
            pool.Add(newPoolObject);
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Removes a item from the pool
    /// </summary>
    /// <returns></returns>
    public virtual TObject Pop(TInfo info)
    {
        // Checks if there's a free object in the pool
        foreach (var item in pool)
        {
            if (item.inPool)
            {
                item.inPool = false;
                item.WakeUp(info);
                return item;
            }
        }

        // If there isn't, create another one and grows the pool
        var newPoolObject = CreateNewPoolObject();
        pool.Add(newPoolObject);
        newPoolObject.inPool = false;
        newPoolObject.WakeUp(info);
        return newPoolObject;
    }

    #endregion
}

/// <summary>
/// Handles a pool of objects
/// </summary>
/// <typeparam name="TPool"></typeparam>
/// <typeparam name="TObject"></typeparam>
public abstract class ObjectPool<TPool, TObject> : MonoBehaviour
    where TPool : ObjectPool<TPool, TObject>
    where TObject : PoolObject<TPool, TObject>, new()
{
    #region Public Properties

    /// <summary>
    /// The prefab for this pool
    /// </summary>
    public GameObject prefab;
    
    /// <summary>
    /// The initial size for the pool
    /// </summary>
    public int initialPoolCount = 10;

    /// <summary>
    /// The reference for the objects pool
    /// </summary>
    [HideInInspector]
    public List<TObject> pool = new List<TObject>();

    #endregion

    #region Unity Methods

    private void Start()
    {
        for (int i = 0; i < initialPoolCount; i++)
        {
            var newPoolObject = CreateNewPoolObject();
            pool.Add(newPoolObject);
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Removes a item from the pool
    /// </summary>
    /// <returns></returns>
    public virtual TObject Pop()
    {
        // Checks if there's a free object in the pool
        foreach (var item in pool)
        {
            if(item.inPool)
            {
                item.inPool = false;
                item.WakeUp();
                return item;
            }
        }

        // If there isn't, create another one and grows the pool
        var newPoolObject = CreateNewPoolObject();
        pool.Add(newPoolObject);
        newPoolObject.inPool = false;
        newPoolObject.WakeUp();
        return newPoolObject;
    }

    /// <summary>
    /// Stores a item in the pool
    /// </summary>
    /// <param name="poolObject">The item to be stored</param>
    public virtual void Push(TObject poolObject)
    {
        poolObject.inPool = true;
        poolObject.Sleep();
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Creates a new object instance and stores on the pool
    /// </summary>
    /// <returns></returns>
    protected TObject CreateNewPoolObject()
    {
        var newPoolObject = new TObject();
        newPoolObject.instance = Instantiate(prefab);
        newPoolObject.instance.transform.SetParent(transform);
        newPoolObject.inPool = true;
        newPoolObject.SetReferences(this as TPool);
        newPoolObject.Sleep();
        return newPoolObject;
    }

    #endregion
}

/// <summary>
/// Creates and handles a object instance inside a pool
/// </summary>
/// <typeparam name="TPool"></typeparam>
/// <typeparam name="TObject"></typeparam>
public abstract class PoolObject<TPool, TObject, TInfo> : PoolObject<TPool, TObject>
        where TPool : ObjectPool<TPool, TObject>
        where TObject : PoolObject<TPool, TObject>, new()
{
    #region Public Methods

    /// <summary>
    /// Event fired when the object is alive
    /// </summary>
    /// <param name="info"></param>
    public virtual void WakeUp(TInfo info) { }

    #endregion
}

/// <summary>
/// Creates and handles a object instance inside a pool
/// </summary>
/// <typeparam name="TPool"></typeparam>
/// <typeparam name="TObject"></typeparam>
[Serializable]
public abstract class PoolObject<TPool, TObject>
        where TPool : ObjectPool<TPool, TObject>
        where TObject : PoolObject<TPool, TObject>, new()
{
    #region Public Properties

    /// <summary>
    /// A flag that represents if the current object is inside the pool
    /// </summary>
    public bool inPool;

    /// <summary>
    /// The object instance
    /// </summary>
    public GameObject instance;

    /// <summary>
    /// A reference for the owner pool
    /// </summary>
    public TPool objectPool;

    #endregion

    #region Public Methods

    /// <summary>
    /// Set the references on the object
    /// </summary>
    /// <param name="pool"></param>
    public void SetReferences(TPool pool)
    {
        objectPool = pool;
        SetReferences();
    }

    /// <summary>
    /// Event fired when the object its alive
    /// </summary>
    public virtual void WakeUp() { }

    /// <summary>
    /// Event fired when the object goes back to the pool
    /// </summary>
    public virtual void Sleep() { }

    /// <summary>
    /// Stores the object on the pool
    /// </summary>
    public virtual void ReturnToPool()
    {
        TObject thisObject = this as TObject;
        objectPool.Push(thisObject);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Set the references on the object
    /// </summary>
    protected virtual void SetReferences() { }

    #endregion
}