using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventReporter : MonoBehaviour
{
    [SerializeField] BabyplumController controller;

    public void AE_StartPattern1()
    {
		controller.PlayPattern1();

	}

    public void AE_StartPattern2()
    {
		controller.PlayPattern2();
	}
   

    public void AE_StartPattern3()
    {
        controller.PlayPattern3();
	}

    public void AE_EnterIdle()
    {
        controller.OnIdleMode();
    }
}

