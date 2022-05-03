using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorManager : MonoBehaviour
{

    [SerializeField]
    [Range(1, 10)]
    private int conveyorMaxCapacity = 7;
    public int ConveyorMaxCapacity { get { return conveyorMaxCapacity; } set { conveyorMaxCapacity = value; } }

    [SerializeField]
    private int conveyorCapacity = 0;
    public int ConveyorCapacity { get { return conveyorCapacity; } set { conveyorCapacity = value; } }

    [SerializeField]
    private ToySpawner myToySpawner;

    private void Awake()
    {
        myToySpawner = GetComponent<ToySpawner>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<ElfActionsManager>().MyConveyor = this;
        }

        if (other.CompareTag("Toys"))
        {
            conveyorCapacity++;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<ElfActionsManager>().MyConveyor = null;
        }

        if (other.CompareTag("Toys"))
        {
            conveyorCapacity--;
            EnableToySpawner();
        }
    }

    private void EnableToySpawner()
    {

        if ((conveyorCapacity < conveyorMaxCapacity) && !myToySpawner.enabled)
        {
            GetComponent<ToySpawner>().enabled = true;
        }
    }

}
