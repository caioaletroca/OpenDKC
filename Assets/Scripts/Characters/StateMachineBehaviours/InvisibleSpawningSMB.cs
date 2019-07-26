using UnityEngine;

/// <summary>
/// The invisible spawn state behaviour for the player controller
/// </summary>
public class InvisibleSpawningSMB : SceneSMB<KongController>
{
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.Damageable.EnableInvulnerability(true);
        mMonoBehaviour.PerformHorizontalMovement(0);
        mMonoBehaviour.DisableRigidBody();
        mMonoBehaviour.SetLocalPosition(Vector2.zero);
    }

    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.Damageable.DisableInvulnerability();
        mMonoBehaviour.EnableRigidBody();
        
    }
}