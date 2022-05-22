using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElfAnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator myAnimator;

    [SerializeField]
    private SpriteRenderer myRenderer;

    [SerializeField]
    [Range(1f, 10f)]
    private float acctionDelay = 1.5f;
    public float AcctionDelay { get { return acctionDelay; } }

    [SerializeField] private bool inAction;
    public bool InAction { get { return inAction; } }

    private void FlipSprite(float xDirection)
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

    public void Walking(float xDirection, float yDirection)
    {
        if ((xDirection == 0) && (yDirection == 0))
        {
            WalkingAnimator(false);
        }
        else
        {
            WalkingAnimator(true);
        }

        FlipSprite(xDirection);
    }

    public void WalkingAnimator(bool status)
    {
        myAnimator.SetBool("isWalking", status);
    }

    public void SlideIdle()
    {
        myAnimator.SetTrigger("Side Idle");
    }

    public void FrontIdle()
    {
        myAnimator.SetTrigger("Front Idle");
    }

    public void AcctionAnimation(string trigger)
    {
        myAnimator.SetTrigger(trigger);
        inAction = true;
        StartCoroutine(ResetAnimation());
    }

    private IEnumerator ResetAnimation()
    {
        yield return new WaitForSeconds(acctionDelay);
        SlideIdle();
        inAction = false;
    }

    public void FlipStatus(bool status)
    {
        myRenderer.flipX = status;
    }
}
