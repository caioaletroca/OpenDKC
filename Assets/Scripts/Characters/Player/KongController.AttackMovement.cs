using UnityEngine;

public partial class KongController
{
    #region Public Properties

    [HideInInspector]
    public bool Attacked = false;

    #endregion

    #region Public Methods

    public void UpdateAttackState()
    {
        PlayerInput.Instance.KongMap.Attack.performed += e => animator.SetBool(AnimationParameters.Attack, true);
        PlayerInput.Instance.KongMap.Attack.canceled += e => animator.SetBool(AnimationParameters.Attack, false);
    }

    public void UpdateRunState()
    {
        PlayerInput.Instance.KongMap.Attack.performed += e => animator.SetBool(AnimationParameters.Run, true);
        PlayerInput.Instance.KongMap.Attack.canceled += e => animator.SetBool(AnimationParameters.Run, false);
    }

    public bool CheckForAttackInput() {
        if (Attacked && !PlayerInput.Instance.Attack)
            Attacked = false;

        return PlayerInput.Instance.Attack && !Attacked;
    }

    public bool CheckForAttackRelease()
    {
        Attacked = PlayerInput.Instance.Attack;
        return Attacked;
    }

    public void PerformAttack()
    {
        Attacked = true;
        
        // Set state variable
        animator.SetBool(AnimationParameters.Attack, true);
        animator.SetBool(AnimationParameters.Run, true);
    }

    public void TransitionToWalk()
    {
        animator.SetBool(AnimationParameters.Attack, false);
        animator.SetBool(AnimationParameters.Run, false);
    }

    public void TransitionToRun()
    {
        animator.SetBool(AnimationParameters.Attack, false);
        animator.SetBool(AnimationParameters.Run, true);
    }

    #endregion
}
