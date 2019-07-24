using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Controls and handles ambient sound and music
/// </summary>
public class BackgroundMusicPlayer : MonoBehaviour
{
    #region Singleton

    /// <summary>
    /// Internal instance for the singleton
    /// </summary>
    protected static BackgroundMusicPlayer mInstance;

    /// <summary>
    /// A singleton instance for the audio controller
    /// </summary>
    public static BackgroundMusicPlayer Instance
    {
        get {
            if (mInstance != null)
                return mInstance;

            mInstance = FindObjectOfType<BackgroundMusicPlayer>();
            return mInstance;
        }
    }

    #endregion

    #region Public Properties
    
    [Header("Music Settings")]

    /// <summary>
    /// The main music <see cref="AudioClip"/>
    /// </summary>
    public AudioClip MusicAudioClip;

    /// <summary>
    /// Mixer group for the music audio
    /// </summary>
    public AudioMixerGroup MusicOutput;

    /// <summary>
    /// The volume for the music audio
    /// </summary>
    [Range(0, 1f)]
    public float MusicVolume = 1f;

    /// <summary>
    /// A flag that represents if the audio should be played on awake method
    /// </summary>
    public bool MusicPlayOnAwake;

    
    [Header("Ambient Settings")]

    /// <summary>
    /// The main ambient <see cref="AudioClip"/>
    /// </summary>
    public AudioClip AmbientAudioClip;

    /// <summary>
    /// Mixer group for the ambient audio
    /// </summary>
    public AudioMixerGroup AmbientOutput;

    /// <summary>
    /// The volume for the ambient audio
    /// </summary>
    [Range(0, 1f)]
    public float AmbientVolume = 1f;

    /// <summary>
    /// A flag that represents if the audio should be played on awake method
    /// </summary>
    public bool AmbientPlayOnAwake;

    #endregion

    #region Private Properties

    /// <summary>
    /// The music stack for playing more than one music
    /// </summary>
    protected Stack<AudioClip> MusicStack = new Stack<AudioClip>();

    /// <summary>
    /// The music audio source
    /// </summary>
    protected AudioSource mMusicAudioSource;

    /// <summary>
    /// The ambient audio source
    /// </summary>
    protected AudioSource mAmbientAudioSource;

    /// <summary>
    /// A flag that represents if we should transfer the music execution
    /// </summary>
    private bool mTransferMusicTime = false;

    /// <summary>
    /// A flag that represents if we should transfer the ambient execution
    /// </summary>
    private bool mTransferAmbientTime = false;

    /// <summary>
    /// A variable to destroy the old sound reproducer
    /// </summary>
    private BackgroundMusicPlayer mOldInstanceToDestroy = null;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        // If there's already a player...
        if (Instance != null && Instance != this)
        {
            //...if it use the same music clip, we set the audio source to be at the same position, so music don't restart
            if (Instance.MusicAudioClip == MusicAudioClip)
                mTransferMusicTime = true;

            //...if it use the same ambient clip, we set the audio source to be at the same position, so ambient don't restart
            if (Instance.AmbientAudioClip == AmbientAudioClip)
                mTransferAmbientTime = true;

            // ... destroy the pre-existing player.
            mOldInstanceToDestroy = Instance;
        }

        mInstance = this;

        DontDestroyOnLoad(gameObject);

        StartAudioSource(out mMusicAudioSource, MusicAudioClip, MusicOutput, MusicVolume, MusicPlayOnAwake);
        StartAudioSource(out mAmbientAudioSource, AmbientAudioClip, AmbientOutput, AmbientVolume, AmbientPlayOnAwake);
    }

    private void Start()
    {
        // if delete & transfer time only in Start so we avoid the small gap that doing everything at the same time in Awake would create 
        if (mOldInstanceToDestroy != null)
        {
            if (mTransferAmbientTime) mAmbientAudioSource.timeSamples = mOldInstanceToDestroy.mAmbientAudioSource.timeSamples;
            if (mTransferMusicTime) mMusicAudioSource.timeSamples = mOldInstanceToDestroy.mMusicAudioSource.timeSamples;
            mOldInstanceToDestroy.Stop();
            Destroy(mOldInstanceToDestroy.gameObject);
        }
    }

    private void Update()
    {
        if(MusicStack.Count > 0)
        {
            // isPlaying will be false once the current clip end up playing
            if (!mMusicAudioSource.isPlaying)
            {
                MusicStack.Pop();
                if(MusicStack.Count > 0)
                {
                    mMusicAudioSource.clip = MusicStack.Peek();
                    mMusicAudioSource.Play();
                }
                else
                {
                    // Back to looping music clip
                    mMusicAudioSource.clip = MusicAudioClip;
                    mMusicAudioSource.loop = true;
                    mMusicAudioSource.Play();
                }
            }
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Adds a new clip to the music stack
    /// </summary>
    /// <param name="clip">The new clip to be added</param>
    public void PushClip(AudioClip clip)
    {
        MusicStack.Push(clip);
        mMusicAudioSource.Stop();
        mMusicAudioSource.loop = false;
        mMusicAudioSource.time = 0;
        mMusicAudioSource.clip = clip;
        mMusicAudioSource.Play();
    }

    /// <summary>
    /// Changes the current main music
    /// </summary>
    /// <param name="clip"></param>
    public void ChangeMusic(AudioClip clip)
    {
        MusicAudioClip = clip;
        mMusicAudioSource.clip = clip;
    }

    /// <summary>
    /// Changes the current ambient music
    /// </summary>
    /// <param name="clip"></param>
    public void ChangeAmbient(AudioClip clip)
    {
        AmbientAudioClip = clip;
        mAmbientAudioSource.clip = clip;
    }

    /// <summary>
    /// Play both audios
    /// </summary>
    public void Play()
    {
        PlayJustAmbient();
        PlayJustMusic();
    }

    /// <summary>
    /// Plays only the music audio
    /// </summary>
    public void PlayJustMusic()
    {
        mMusicAudioSource.time = 0f;
        mMusicAudioSource.Play();
    }
    
    /// <summary>
    /// Plays only the ambient audio
    /// </summary>
    public void PlayJustAmbient()
    {
        mAmbientAudioSource.Play();
    }

    /// <summary>
    /// Stops both audios
    /// </summary>
    public void Stop()
    {
        StopJustAmbient();
        StopJustMusic();
    }

    /// <summary>
    /// Stops only the music audio
    /// </summary>
    public void StopJustMusic()
    {
        mMusicAudioSource.Stop();
    }

    /// <summary>
    /// Stops only the ambient audio
    /// </summary>
    public void StopJustAmbient()
    {
        mAmbientAudioSource.Stop();
    }

    public void Mute()
    {
        MuteJustAmbient();
        MuteJustMusic();
    }

    public void MuteJustMusic()
    {
        mMusicAudioSource.volume = 0f;
    }

    public void MuteJustAmbient()
    {
        mAmbientAudioSource.volume = 0f;
    }

    public void Unmute()
    {
        UnmuteJustAmbient();
        UnmuteJustMustic();
    }

    public void UnmuteJustMustic()
    {
        mMusicAudioSource.volume = MusicVolume;
    }

    public void UnmuteJustAmbient()
    {
        mAmbientAudioSource.volume = AmbientVolume;
    }

    public void Mute(float fadeTime)
    {
        MuteJustAmbient(fadeTime);
        MuteJustMusic(fadeTime);
    }

    public void MuteJustMusic(float fadeTime)
    {
        StartCoroutine(VolumeFade(mMusicAudioSource, 0f, fadeTime));
    }

    public void MuteJustAmbient(float fadeTime)
    {
        StartCoroutine(VolumeFade(mAmbientAudioSource, 0f, fadeTime));
    }

    public void Unmute(float fadeTime)
    {
        UnmuteJustAmbient(fadeTime);
        UnmuteJustMusic(fadeTime);
    }

    public void UnmuteJustMusic(float fadeTime)
    {
        StartCoroutine(VolumeFade(mMusicAudioSource, MusicVolume, fadeTime));
    }

    public void UnmuteJustAmbient(float fadeTime)
    {
        StartCoroutine(VolumeFade(mAmbientAudioSource, AmbientVolume, fadeTime));
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Starts a <see cref="AudioSource"/> with the parameters passed to the object
    /// </summary>
    /// <param name="source"></param>
    /// <param name="clip"></param>
    /// <param name="mixer"></param>
    /// <param name="volume"></param>
    /// <param name="playOnAwake"></param>
    protected void StartAudioSource(out AudioSource source, AudioClip clip, AudioMixerGroup mixer, float volume, bool playOnAwake = false)
    {
        source = gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.outputAudioMixerGroup = mixer;
        source.loop = true;
        source.volume = volume;

        if (playOnAwake && clip != null)
        {
            source.time = 0f;
            source.Play();
        }
    }

    /// <summary>
    /// Fades a volume from a <see cref="AudioSource"/>
    /// </summary>
    /// <param name="source">The source to be fadded</param>
    /// <param name="finalVolume">The final volume for the sound</param>
    /// <param name="fadeTime">The total time for the effect</param>
    /// <returns></returns>
    protected IEnumerator VolumeFade(AudioSource source, float finalVolume, float fadeTime)
    {
        float volumeDifference = Mathf.Abs(source.volume - finalVolume);
        float inverseFadeTime = 1f / fadeTime;

        while(!Mathf.Approximately(source.volume, finalVolume))
        {
            float delta = Time.deltaTime * volumeDifference * inverseFadeTime;
            source.volume = Mathf.MoveTowards(source.volume, finalVolume, delta);
            yield return null;
        }
        source.volume = finalVolume;
    }

    #endregion
}
