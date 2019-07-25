using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class VFX
{
    #region Public Properties

    /// <summary>
    /// Prefab type for this vfx
    /// </summary>
    public GameObject prefab;

    /// <summary>
    /// The total lifetime duration for the vfx
    /// </summary>
    public float lifetime = 1;

    /// <summary>
    /// Instance for the used pool
    /// </summary>
    [NonSerialized]
    public VFXInstancePool pool;

    //public Dictionary<Ti>

    #endregion
}

/// <summary>
/// The VFX instance itself
/// </summary>
public class VFXInstance : PoolObject<VFXInstancePool, VFXInstance>, IComparable<VFXInstance>
{
    #region Public Properties

    /// <summary>
    /// The time when the vfx expires and finishs
    /// </summary>
    public float expires;

    /// <summary>
    /// Instance for the animation component
    /// </summary>
    public Animation animation;

    /// <summary>
    /// Instance for the audio source component
    /// </summary>
    public AudioSource audioSource;

    /// <summary>
    /// A array of instances for particle systems
    /// </summary>
    public ParticleSystem[] particleSystems;

    /// <summary>
    /// The transform for the vfx
    /// </summary>
    public Transform transform;

    /// <summary>
    /// The transform of the parent
    /// </summary>
    public Transform parent;

    #endregion

    #region Pool Methods

    protected override void SetReferences()
    {
        transform = instance.transform;
        animation = instance.GetComponentInChildren<Animation>();
        audioSource = instance.GetComponentInChildren<AudioSource>();
        particleSystems = instance.GetComponentsInChildren<ParticleSystem>();
    }

    public override void WakeUp()
    {
        instance.SetActive(true);
        foreach (var particleSystem in particleSystems)
            particleSystem.Play();
        if(animation != null)
        {
            animation.Rewind();
            animation.Play();
        }
        if (audioSource != null)
            audioSource.Play();
    }

    public override void Sleep()
    {
        foreach (var particleSystem in particleSystems)
            particleSystem.Stop();
        if (animation != null)
        {
            animation.Rewind();
            animation.Stop();
        }
        if (audioSource != null)
            audioSource.Stop();
        instance.SetActive(false);
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Sets the position of the vfx
    /// </summary>
    /// <param name="position">The new position</param>
    public void SetPosition(Vector3 position) => transform.localPosition = position;

    /// <summary>
    /// Compares vfxs using their expiration time
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public int CompareTo(VFXInstance other) => expires.CompareTo(other.expires);

    #endregion
}

public class VFXInstancePool : ObjectPool<VFXInstancePool, VFXInstance>
{

}

/// <summary>
/// The master VFX controller and object pools
/// </summary>
public class VFXController : MonoBehaviour
{
    #region Singleton

    /// <summary>
    /// The internal instance for the singleton
    /// </summary>
    protected static VFXController mInstance;

    /// <summary>
    /// A public instance singleton
    /// </summary>
    public static VFXController Instance
    {
        get
        {
            if (mInstance != null)
                return mInstance;

            // Find in the scene
            mInstance = FindObjectOfType<VFXController>();

            return mInstance;
        }
    }

    #endregion

    #region Types

    /// <summary>
    /// A structure to define pending vfx requests
    /// </summary>
    struct PendingVFX : IComparable<PendingVFX>
    {
        public VFX vfx;
        public Vector3 position;
        public float startAt;
        public bool flip;
        public Transform parent;
        public TileBase tileOverride;

        public int CompareTo(PendingVFX other) => startAt.CompareTo(other.startAt);
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// All VFX instances for the game
    /// </summary>
    public VFX[] vfxConfig;

    #endregion

    #region Private Properties

    /// <summary>
    /// The dictionary for all pools in the game
    /// </summary>
    Dictionary<int, VFX> mFXPools = new Dictionary<int, VFX>();

    /// <summary>
    /// A fast access queue for current running vfxs
    /// </summary>
    PriorityQueue<VFXInstance> mRunningFX = new PriorityQueue<VFXInstance>();

    /// <summary>
    /// A fast access queue for pending vfxs
    /// </summary>
    PriorityQueue<PendingVFX> mPendingFX = new PriorityQueue<PendingVFX>();

    #endregion

    #region Unity Methods

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (var vfx in vfxConfig)
        {
            // Creates the pool
            vfx.pool = gameObject.AddComponent<VFXInstancePool>();
            vfx.pool.initialPoolCount = 2;
            vfx.pool.prefab = vfx.prefab;

            // Add to the list of pools
            mFXPools[StringToHash(vfx.prefab.name)] = vfx;
        }
    }

    private void Update()
    {
        // Check for finished vfxs
        while (!mRunningFX.Empty && mRunningFX.First.expires <= Time.time)
        {
            var instance = mRunningFX.Pop();
            instance.objectPool.Push(instance);
        }
        
        // Check if should start any pending vfxs
        while(!mPendingFX.Empty && mPendingFX.First.startAt <= Time.time)
        {
            var task = mPendingFX.Pop();
            CreateInstance(task.vfx, task.position, task.flip, task.parent, task.tileOverride);
        }

        // Updates attached to parent vfxs
        var instances = mRunningFX.items;
        foreach (var vfx in instances)
        {
            if (vfx.parent != null)
                vfx.transform.position = vfx.parent.position;
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Triggers an instantiation of VFX
    /// </summary>
    /// <param name="name"></param>
    /// <param name="position"></param>
    /// <param name="startDelay"></param>
    /// <param name="flip"></param>
    /// <param name="parent"></param>
    /// <param name="tileOverride"></param>
    public void Trigger(string name, Vector3 position, float startDelay, bool flip, Transform parent, TileBase tileOverride = null)
    {
        Trigger(StringToHash(name), position, startDelay, flip, parent, tileOverride);
    }

    /// <summary>
    /// Triggers an instantiation of VFX
    /// </summary>
    /// <param name="hash"></param>
    /// <param name="position"></param>
    /// <param name="startDelay"></param>
    /// <param name="flip"></param>
    /// <param name="parent"></param>
    /// <param name="tileOverride"></param>
    public void Trigger(int hash, Vector3 position, float startDelay, bool flip, Transform parent, TileBase tileOverride = null)
    {
        VFX vfx;
        if (!mFXPools.TryGetValue(hash, out vfx))
            Debug.LogError("VFX does not exist.");
        else
        {
            if (startDelay > 0)
                mPendingFX.Push(new PendingVFX()
                {
                    vfx = vfx,
                    position = position,
                    startAt = Time.time + startDelay,
                    flip = flip,
                    parent = parent,
                    tileOverride = tileOverride,
                });
            else
                CreateInstance(vfx, position, flip, parent, tileOverride);
        }
        
    }

    /// <summary>
    /// Converts a string name to its hash code
    /// </summary>
    /// <param name="name">The string to be converted</param>
    /// <returns></returns>
    public static int StringToHash(string name) => name.GetHashCode();

    #endregion

    #region Private Methods

    /// <summary>
    /// Creates a new instance for a object pool VFX
    /// </summary>
    /// <param name="vfx"></param>
    /// <param name="position"></param>
    /// <param name="flip"></param>
    /// <param name="parent"></param>
    /// <param name="tileOverride"></param>
    protected void CreateInstance(VFX vfx, Vector4 position, bool flip, Transform parent, TileBase tileOverride)
    {
        VFXInstancePool poolToUse = null;

        poolToUse = vfx.pool;

        // Get the object instance
        var instance = poolToUse.Pop();

        // Set the maximum time for the vfx
        instance.expires = Time.time + vfx.lifetime;

        // Flip the vfx if necessary
        if (flip)
            instance.transform.localScale = new Vector3(-1, 1, 1);
        else
            instance.transform.localScale = new Vector3(1, 1, 1);

        // Sets the instance properties
        instance.parent = parent;
        instance.SetPosition(position);
        mRunningFX.Push(instance);
    }

    #endregion
}