using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("PLAYER IN CONVEYOR");
            other.GetComponent<ElfActionsManager>().MyConveyor = this;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("PLAYER OUT CONVEYOR");
            ElfActionsManager elfActionsManager = other.GetComponent<ElfActionsManager>();
            elfActionsManager.MyConveyor = null;
            elfActionsManager.NextToy = null;
        }

    }
}
