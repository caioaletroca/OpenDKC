using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class KaboingGreyRisingSMB : DelayedSceneSMB<KaboingGrey>
{
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.PerformJump();
    }

    public override void OnSLStateEnterDelayed(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
