using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElfCollisions : MonoBehaviour
{
    [SerializeField]
    private ElfActionsManager myActionsManager = null;

    private void Awake()
    {
        myActionsManager = GetComponent<ElfActionsManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("ToyControls"))
        {
            myActionsManager.MyElfAnimation.FrontIdle();
            myActionsManager.NextToy = other.transform.parent.gameObject;
        }

        if (other.CompareTag("TalkControls"))
        {
            myActionsManager.MyElfAnimation.FrontIdle();
            myActionsManager.MyInterlocutor = other.transform.parent.GetComponent<Elf>();
        }

        if (other.CompareTag("VendingMachines"))
        {
            myActionsManager.MyElfAnimation.FrontIdle();
            myActionsManager.MyVendingMachine = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
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
