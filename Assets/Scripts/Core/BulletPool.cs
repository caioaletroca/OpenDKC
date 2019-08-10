using System.Collections.Generic;
using UnityEngine;

public class BulletPool : ObjectPool<BulletPool, BulletObject, Vector2>
{
    #region Private Properties

    /// <summary>
    /// List containing instances for all bullet pools
    /// </summary>
    protected static Dictionary<GameObject, BulletPool> mPoolInstances = new Dictionary<GameObject, BulletPool>();

    #endregion

    #region Unity Methods

    private void Awake()
    {
        //This allow to make Pool manually added in the scene still automatically findable & usable
        if (prefab != null && !mPoolInstances.ContainsKey(prefab))
            mPoolInstances.Add(prefab, this);
    }

    private void OnDestroy()
    {
        mPoolInstances.Remove(prefab);
    }

    #endregion

    #region Pool Methods

    public static BulletPool GetObjectPool(GameObject prefab, int initialPoolCount = 10)
    {
        BulletPool objPool = null;
        if(!mPoolInstances.TryGetValue(prefab, out objPool))
        {
            var obj = new GameObject(prefab.name + "_Pool");
            objPool = obj.AddComponent<BulletPool>();
            objPool.prefab = prefab;
            objPool.initialPoolCount = initialPoolCount;

            mPoolInstances[prefab] = objPool;
        }

        return objPool;
    }

    #endregion
}

public class BulletObject : PoolObject<BulletPool, BulletObject, Vector2>
{
    #region Public Properties

    /// <summary>
    /// Transform for the bullet
    /// </summary>
    public Transform transform;

    /// <summary>
    /// RigidBody2D for the bullet movement
    /// </summary>
    public Rigidbody2D rigidBody2D;

    /// <summary>
    /// Bullet instance
    /// </summary>
    public Bullet bullet;

    #endregion

    #region Pool Methods

    protected override void SetReferences()
    {
        transform = instance.transform;
        rigidBody2D = instance.GetComponent<Rigidbody2D>();
        bullet = instance.GetComponent<Bullet>();
        bullet.bulletPoolObject = this;
    }

    public override void WakeUp(Vector2 position)
    {
        transform.position = position;
        instance.SetActive(true);
    }

    public override void Sleep()
    {
        instance.SetActive(false);
    }

    #endregion
}
