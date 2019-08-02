using UnityEngine;

/// <summary>
/// Controls the walking state for the Klampon
/// </summary>
public class KlamponWalkingSMB : SceneSMB<Klampon>
{
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.Bite = false;
    }
}
