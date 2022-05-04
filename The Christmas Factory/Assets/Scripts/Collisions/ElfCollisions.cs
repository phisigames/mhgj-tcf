using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElfCollisions : MonoBehaviour
{
    [SerializeField] private ElfActionsManager myActionsManager = null;

    private void Awake()
    {
        myActionsManager = GetComponent<ElfActionsManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (myActionsManager.NextToy != null) { return; }
        AddToyReference(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Toys"))
        {
            myActionsManager.NextToy = null;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (myActionsManager.NextToy != null) { return; }
        AddToyReference(other);
    }

    private void AddToyReference(Collider2D other)
    {
        if (other.CompareTag("Toys"))
        {
            if (myActionsManager.NextToy == null)
            {
                myActionsManager.NextToy = other.gameObject;
            }
        }
    }
}
