using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Controls all the movement when the player is on Hook Mode
/// </summary>
public class HookController : MonoBehaviour
{
    #region Public Properties

    /// <summary>
    /// A instance for the animator
    /// </summary>
    [SerializeField]
    private Animator animator;

    /// <summary>
    /// The force applied to the jump
    /// </summary>
    [SerializeField]
    private float JumpForce = 400f;

    /// <summary>
    /// The offset where the sprite should snap as a <see cref="Vector3"/>
    /// </summary>
    [SerializeField]
    private Vector3 SnapOffset = Vector3.zero;

    #endregion

    #region Public Events

    [Header("Events")]
    [Space]

    /// <summary>
    /// Fires whenever the player touches the ground
    /// </summary>
    public UnityEvent OnLandEvent;

    #endregion

    #region Private Properties

    /// <summary>
    /// The object <see cref="Rigidbody2D"/>
    /// </summary>
    private Rigidbody2D mRigidbody2D;

    /// <summary>
    /// The current hook the player is attachted to
    /// </summary>
    private GameObject Hook;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        mRigidbody2D = GetComponent<Rigidbody2D>();
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Starts the current game mode
    /// </summary>
    /// <param name="hook"></param>
    public void Initialize(GameObject hook)
    {
        // Instantiate the current hook
        Hook = hook;

        // Set the new state
        PlayerController.Instance.CurrentState = PlayerController.State.Hook;

        // Call animations
        animator.SetBool("Hook", true);
        animator.SetTrigger("HookTrigger");
    }

    /// <summary>
    /// Calculate movement for the character
    /// </summary>
    /// <param name="move"></param>
    /// <param name="Run"></param>
    /// <param name="crouch"></param>
    /// <param name="jump"></param>
    public void Move(Vector2 move, bool Run, bool crouch, bool jump)
    {
        // Disable Gravity
        transform.position = Hook.transform.position + SnapOffset;
        mRigidbody2D.velocity = Vector2.zero;
        
        // If the character should jump
        if (jump)
        {
            // Add vertical force to the player
            mRigidbody2D.AddForce(new Vector2(0f, JumpForce));

            // Move the character by finding the target velocity
            Vector2 TargetVelocity = new Vector2(move.x * 10f, mRigidbody2D.velocity.y);

            // TODO: Solve horizontal velocity when exiting the hook

            Deactivate();
        }
    }

    /// <summary>
    /// Deactivate this current controller
    /// </summary>
    public void Deactivate()
    {
        // Reset the current state
        PlayerController.Instance.CurrentState = PlayerController.State.Normal;

        // Call the landing event
        OnLandEvent.Invoke();

        animator.SetBool("Hook", false);
    }

    #endregion

    #region Unity Events

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hook")
            Initialize(collision.gameObject);
    }

    #endregion
}
