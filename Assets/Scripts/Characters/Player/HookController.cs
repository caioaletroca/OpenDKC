using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Controls all the movement when the player is on Hook Mode
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class HookController : BaseController
{
    //#region Private Properties

    ///// <summary>
    ///// A instance for the animator
    ///// </summary>
    //private Animator animator;

    ///// <summary>
    ///// The force applied to the jump
    ///// </summary>
    //[SerializeField]
    //private float JumpForce = 400f;

    ///// <summary>
    ///// The offset where the sprite should snap as a <see cref="Vector3"/>
    ///// </summary>
    //[SerializeField]
    //private Vector3 SnapOffset = Vector3.zero;

    //#endregion

    //#region Public Events

    //[Header("Events")]
    //[Space]

    ///// <summary>
    ///// Fires whenever the player touches the ground
    ///// </summary>
    //public UnityEvent OnLandEvent;

    //#endregion

    //#region Private Properties

    ///// <summary>
    ///// The object <see cref="Rigidbody2D"/>
    ///// </summary>
    //private Rigidbody2D mRigidbody2D;

    ///// <summary>
    ///// The current hook the player is attachted to
    ///// </summary>
    //private GameObject Hook;

    //#endregion

    //#region Unity Methods

    //private void Awake()
    //{
    //    mRigidbody2D = GetComponent<Rigidbody2D>();
    //    animator = GetComponent<Animator>();
    //}

    //private void FixedUpdate()
    //{
    //    if (KongController.Instance.CurrentController == this)
    //    {
            
    //    }
    //}

    //#endregion

    //#region Public Methods

    ///// <summary>
    ///// Starts the current game mode
    ///// </summary>
    ///// <param name="hook"></param>
    //public void Initialize(GameObject hook)
    //{
    //    // Instantiate the current hook
    //    Hook = hook;

    //    // Set the new state
    //    KongController.Instance.CurrentController = this;

    //    // Call animations
    //    animator.SetBool("Hooking", true);
    //    animator.SetTrigger("HookTrigger");
    //}

    ///// <summary>
    ///// Deactivate this current controller
    ///// </summary>
    //public void Deactivate()
    //{
    //    // Reset the current state
    //    KongController.Instance.CurrentController = KongController.Instance.runController;

    //    // Call the landing event
    //    OnLandEvent.Invoke();

    //    animator.SetBool("Hooking", false);
    //}

    //#endregion

    //#region Action Methods

    //public override void HorinzontalAxis(Vector2 movement, bool run = false, bool crouch = false)
    //{
    //    transform.position = Hook.transform.position + SnapOffset;
    //    mRigidbody2D.velocity = Vector2.zero;
    //}

    //public override void JumpButton()
    //{
    //    // Add vertical force to the player
    //    mRigidbody2D.AddForce(new Vector2(0f, JumpForce));

    //    // TODO: Solve horizontal velocity when exiting the hook

    //    Deactivate();
    //}

    //#endregion

    //#region Unity Events

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Hook")
    //        Initialize(collision.gameObject);
    //}

    //#endregion
}
