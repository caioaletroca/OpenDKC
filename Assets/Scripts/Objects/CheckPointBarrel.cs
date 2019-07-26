using UnityEngine;

public class CheckPointBarrel : MonoBehaviour
{
    #region State Variables

    /// <summary>
    /// A flag that represents if the barrel is spawning the Player
    /// </summary>
    [HideInInspector]
    public bool Spawn
    {
        get
        {
            return animator.GetBool("Spawn");
        }
        set
        {
            animator.SetBool("Spawn", value);
        }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Instance for the game object animator
    /// </summary>
    private Animator animator;

    #endregion

    #region Unity Events

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    #endregion

    #region Public Methods

    public void OnSpawnStart()
    {
        Spawn = true;

        KongController.Instance.SetLocalPosition(Vector2.zero);
        KongController.Instance.SetParent(gameObject);
    }

    public void OnSpawnFinished()
    {   
        // Show player
        KongController.Instance.Spawn = false;

        KongController.Instance.SetParent(null);

        // Destroy it self
        Destroy(gameObject);
    }

    #endregion
}
