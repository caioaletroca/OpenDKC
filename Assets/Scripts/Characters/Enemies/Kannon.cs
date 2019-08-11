using UnityEngine;

/// <summary>
/// Constrols the Kannon enemy
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class Kannon : Enemy
{
    #region State Variables

    /// <summary>
    /// The Fire trigger state variable
    /// </summary>
    public void FireTrigger() => animator.SetTrigger("FireTrigger");

    #endregion

    #region Public Properties

    /// <summary>
    /// Instance for the Kannon Controller
    /// </summary>
    public MonoBehaviour KannonController;

    #endregion

    #region Private Properties

    /// <summary>
    /// Instance for the audio source component
    /// </summary>
    protected AudioSource audioSource;

    #endregion

    #region Unity Methods

    protected new void Awake()
    {
        base.Awake();

        audioSource = GetComponent<AudioSource>();
    }

    #endregion

    #region Event Methods

    public override void OnActivateEvent()
    {
        base.OnActivateEvent();

        KannonController.enabled = true;
    }

    public override void OnDeactivateEvent()
    {
        base.OnDeactivateEvent();

        KannonController.enabled = false;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Performs the shoot sound effect
    /// </summary>
    public void PerformShootSound() => audioSource.Play();

    #endregion
}
