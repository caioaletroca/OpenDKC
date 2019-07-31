using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BarrelRotateSMB : SceneSMB<RotativeBarrel>
{
    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mMonoBehaviour.PerformBlastTimer();
        mMonoBehaviour.UpdateDirection();
        mMonoBehaviour.UpdateBlastDiretion();

        // Check for state change
        if (mMonoBehaviour.CheckForBlastTimer())
            mMonoBehaviour.PerformAutoBlast();
    }
}
