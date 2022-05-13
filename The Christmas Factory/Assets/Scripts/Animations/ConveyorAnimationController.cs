using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorAnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator myAnimator;

    public void TriggerAnimation(string trigger)
    {
        myAnimator.SetTrigger(trigger);
    }
}
