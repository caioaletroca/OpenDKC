using UnityEngine;

/// <summary>
/// Controls the bitten enemy
/// </summary>
public class Klampon : Neek
{
    #region State Variable

    /// <summary>
    /// Represents if klampon has bitten the player
    /// </summary>
    public bool Bite
    {
        get => animator.GetBool("Bite");
        set => animator.SetBool("Bite", value);
    }

    #endregion

    #region Private Properties

    /// <summary>
    /// Instance for the audio source
    /// </summary>
    protected AudioSource audioSource;

    #endregion

    #region Unity Methods

    private new void Awake()
    {
        base.Awake();

        audioSource = GetComponent<AudioSource>();

        // Start state machine
        SceneSMB<Klampon>.Initialise(animator, this);
    }

    private new void FixedUpdate()
    {
        // Do not move if in bite state
        if (Bite)
            return;

        var direction = transform.localScale.x * -1;

        mRigidBody2D.velocity = new Vector2(direction * Speed, mRigidBody2D.velocity.y);
    }

    #endregion

    #region Perform Methods

    /// <summary>
    /// Performs the bite sound effect
    /// </summary>
    public void PerformBiteSound() => audioSource.Play();

    #endregion
}

