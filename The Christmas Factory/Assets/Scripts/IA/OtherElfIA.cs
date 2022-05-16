using UnityEngine;

public class OtherElfIA : MonoBehaviour
{
    [SerializeField]
    private ElfActionsManager myActionsManager = null;

    void Start()
    {
        myActionsManager = GetComponent<ElfActionsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!myActionsManager.MyElfAnimation.InAction)
        {
            myActionsManager.ToyWrapping();
        }
    }
}
