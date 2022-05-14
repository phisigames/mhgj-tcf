using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorAnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator myAnimator;

    [SerializeField]
    private Animator myReelAnimator;

    public void TriggerAnimation(string trigger)
    {
        myAnimator.SetTrigger(trigger);
    }

    public void TriggerReelAnimation(string trigger)
    {
        myReelAnimator.SetTrigger(trigger);
    }
}
