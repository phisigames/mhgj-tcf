using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElfCollisions : MonoBehaviour
{
    [SerializeField]
    private ElfActionsManager myActionsManager = null;

    [SerializeField]
    private ElfAnimationController myElfAnimation = null;

    private void Awake()
    {
        myActionsManager = GetComponent<ElfActionsManager>();
        myElfAnimation = GetComponent<ElfAnimationController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (!myElfAnimation.InAction)
        {
            myElfAnimation.FrontIdle();
        }

        if (other.CompareTag("ToyControls"))
        {
            myActionsManager.NextToy = other.transform.parent.gameObject;
        }

        if (other.CompareTag("TalkControls"))
        {
            myActionsManager.MyInterlocutor = other.transform.parent.GetComponent<Elf>();
        }

        if (other.CompareTag("VendingMachines"))
        {
            myActionsManager.MyVendingMachine = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (!myElfAnimation.InAction)
        {
            myElfAnimation.SlideIdle();
        }

        if (other.CompareTag("ToyControls"))
        {
            myActionsManager.NextToy = null;
        }

        if (other.CompareTag("TalkControls"))
        {
            myActionsManager.MyInterlocutor = null;
        }

        if (other.CompareTag("VendingMachines"))
        {
            myActionsManager.MyVendingMachine = null;
        }
    }
}
