using UnityEngine;

/// <summary>
/// Controls the neek enemy
/// </summary>
public class Neek : Enemy
{
    #region Public Properties

    /// <summary>
    /// The walking speed for the object
    /// </summary>
    public float Speed = 5;

    /// <summary>
    /// The elapsed time to delay between loops
    /// </summary>
    [Tooltip("The elapsed time to delay the sound effect between loops")]
    public float LoopDelay = 4;

    #endregion

    #region Private Properties

    /// <summary>
    /// Instance for the audio source
    /// </summary>
    protected AudioSource audioSource;

    /// <summary>
    /// Stores the timer to execute the sound
    /// </summary>
    protected float AudioTimer;

    #endregion

    #region Unity Methods

    private new void Awake()
    {
        base.Awake();

        audioSource = GetComponent<AudioSource>();

        // Enables damager
        Damager.Enable();
    }

    private void Update()
    {
        // Decrement timer
        AudioTimer -= Time.deltaTime;

        if (AudioTimer < 0)
        {
            // Restart timer
            AudioTimer = LoopDelay;

            // Play sound
            audioSource.Play();
        }
    }

    private void FixedUpdate()
    {  
        var direction = transform.localScale.x * -1;

        mRigidBody2D.velocity = new Vector2(direction * Speed, mRigidBody2D.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Controls the flip behaviour when hitting a wall
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
            Flip();
    }

    #endregion

    #region Event Methods

    /// <summary>
    /// Fired when the enemy dies
    /// </summary>
    /// <param name="damager"></param>
    /// <param name="damageable"></param>
    public override void OnDieEvent(Damager damager, Damageable damageable)
    {
        // Set state variable
        Die = true;

        DisableEnemy();
        PerformDeathJump(damageable.DamageDirection);

        // Despawn in time
        Destroy(gameObject, TimeToDespawn);
    }

    #endregion
}
