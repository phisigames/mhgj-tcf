using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElfActionsManager : MonoBehaviour
{
    [SerializeField] private Elf elfData = null;
    public Elf ElfData { get { return elfData; } }

    [SerializeField] private ConveyorManager myConveyor = null;
    public ConveyorManager MyConveyor { get { return myConveyor; } set { myConveyor = value; } }

    [SerializeField] private Elf myInterlocutor = null;
    public Elf MyInterlocutor { get { return myInterlocutor; } set { myInterlocutor = value; } }

    [SerializeField] private GameObject nextToy = null;
    public GameObject NextToy { get { return nextToy; } set { nextToy = value; } }

    [SerializeField] private GameObject myVendingMachine = null;
    public GameObject MyVendingMachine { get { return myVendingMachine; } set { myVendingMachine = value; } }

    [SerializeField] private CalloutManager myCalloutManager = null;
    public CalloutManager MyCalloutManager { get { return myCalloutManager; } }

    [SerializeField]
    private ElfAnimationController myElfAnimation = null;

    private void Awake()
    {
        myElfAnimation = GetComponent<ElfAnimationController>();
    }


    void Update()
    {
        ToyWrapping();
        Talk();
        HavingCoffee();
    }

    private void ToyWrapping()
    {
        if (nextToy == null) { return; }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("WRAPPING TOY");
            myCalloutManager.SetCalloutSprite(CalloutTypes.Wrap);
            myCalloutManager.EnableCallout(true);
            ElfData.GiftWrapping++;
            if (ElfData.isLimitToWrap())
            {
                ElfData.GiftWrapping = 0;
                StressManager.Instance.CumulativeStress++;
                //REMPLACE WITH EVENT SOLUTION
                FindObjectOfType<HUD>().UpdateStressBar();
            }
            myElfAnimation.AcctionAnimation("Wrap");
            WrapSequence();
        }
    }

    private void Talk()
    {
        if (myInterlocutor == null) { return; }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("TALK");
            if (ElfData.canTalk())
            {
                Debug.Log("TALKING TO ELF");
                myElfAnimation.AcctionAnimation("Talk");
                StartCoroutine(TalkSequence());
            }
        }
    }

    private void HavingCoffee()
    {
        if (myVendingMachine == null) { return; }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("DRINKING COFFEE");
            myElfAnimation.AcctionAnimation("Coffee");
            StartCoroutine(CoffeeSequence());
        }
    }

    private void WrapSequence()
    {
        myCalloutManager.EnableCallout(false);
        if (nextToy.activeSelf)
        {
            nextToy.SetActive(false);
            nextToy = null;
        }
    }

    private IEnumerator TalkSequence()
    {
        myCalloutManager.SetCalloutSprite(CalloutTypes.Talk);
        myCalloutManager.EnableCallout(true);
        //ACA EFECTOS DE CHARLA
        for (int i = 0; i < ElfData.ResistenceToTalk; i++)
        {
            yield return new WaitForSeconds(1f);
            ElfData.TalkTime++;
            Debug.Log("SEGUNDO DE CHARLA " + ElfData.TalkTime);
        }
        ElfData.TalkTime = 0;
        Debug.Log("RESET TALK");
        myCalloutManager.EnableCallout(false);
        StressManager.Instance.CumulativeStress--;
        //REMPLACE WITH EVENT SOLUTION
        FindObjectOfType<HUD>().UpdateStressBar();
    }

    private IEnumerator CoffeeSequence()
    {
        myCalloutManager.SetCalloutSprite(CalloutTypes.Coffee);
        myCalloutManager.EnableCallout(true);
        yield return new WaitForSeconds(1f);
        StressManager.Instance.CumulativeStress--;
        myVendingMachine = null;
        myCalloutManager.EnableCallout(false);
        //REMPLACE WITH EVENT SOLUTION
        FindObjectOfType<HUD>().UpdateStressBar();
    }
}
