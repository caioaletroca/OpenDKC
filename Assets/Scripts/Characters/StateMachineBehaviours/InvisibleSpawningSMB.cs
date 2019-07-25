using UnityEngine;

/// <summary>
/// The invisible spawn state behaviour for the player controller
/// </summary>
public class InvisibleSpawningSMB : SceneSMB<KongController>
{
    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.PerformHorizontalMovement(0);
        mMonoBehaviour.SetLocalPosition(mMonoBehaviour.SpawnGameObject.transform.position);
    }
}