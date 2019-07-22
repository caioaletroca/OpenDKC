using UnityEngine;

public class InvisibleSpawningSMB : SceneSMB<KongController>
{
    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.PerformHorizontalMovement(0);
        mMonoBehaviour.Teleport(mMonoBehaviour.CheckPointBarrelGameObject.transform.position);
    }
}