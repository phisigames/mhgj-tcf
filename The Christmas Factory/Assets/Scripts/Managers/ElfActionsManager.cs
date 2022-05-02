using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElfActionsManager : MonoBehaviour
{
    [SerializeField] private EndpointManager myEndpoint = null;
    public EndpointManager MyEndpoint { get { return myEndpoint; } set { myEndpoint = value; } }

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
            Debug.Log("WRAP TOY");
            Destroy(nextToy);
            nextToy = null;
        }
    }
}
