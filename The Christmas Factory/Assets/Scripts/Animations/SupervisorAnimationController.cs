using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupervisorAnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator myAnimator;

    [SerializeField]
    private SpriteRenderer myRenderer;

    public void FlipSprite(float xDirection)
    {
        if (xDirection > 0)
        {
            myRenderer.flipX = false;
        }
        else if (xDirection < 0)
        {
            myRenderer.flipX = true;
        }
    }

    public void AcctionAnimation(string trigger)
    {
        myAnimator.SetTrigger(trigger);
    }
}
