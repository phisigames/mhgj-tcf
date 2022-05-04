using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElfActionsManager : MonoBehaviour
{
    [SerializeField] private ConveyorManager myConveyor = null;
    public ConveyorManager MyConveyor { get { return myConveyor; } set { myConveyor = value; } }

    [SerializeField] private ElfActionsManager myInterlocutor = null;
    public ElfActionsManager MyInterlocutor { get { return myInterlocutor; } set { myInterlocutor = value; } }

    [SerializeField] private GameObject nextToy = null;
    public GameObject NextToy { get { return nextToy; } set { nextToy = value; } }

    void Update()
    {
        ToyWrapping();
    }

    private void ToyWrapping()
    {
        if (nextToy == null) { return; }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("WRAPPING TOY");
            Invoke("WrapSequence", 0.1f);
        }
    }

    private void WrapSequence()
    {
        if (nextToy.activeSelf)
        {
            nextToy.SetActive(false);
            nextToy = null;
        }
    }
}
