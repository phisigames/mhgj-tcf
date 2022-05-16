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
    public ElfAnimationController MyElfAnimation { get { return myElfAnimation; } }

    private void Awake()
    {
        myElfAnimation = GetComponent<ElfAnimationController>();
    }

    public void ToyWrapping()
    {
        if (nextToy == null) { return; }
        Debug.Log("WRAPPING TOY");
        //myCalloutManager.SetCalloutSprite(CalloutTypes.Wrap);
        //myCalloutManager.EnableCallout(true);
        myElfAnimation.AcctionAnimation("Wrap");
        ElfData.GiftWrapping++;
        if (ElfData.isLimitToWrap())
        {
            ElfData.GiftWrapping = 0;
            StressManager.IncreaseStress(1);
        }

        if (nextToy.activeSelf)
        {
            nextToy.SetActive(false);
            nextToy = null;
        }
        StartCoroutine(WrapSequence());

    }

    public void Talk()
    {
        if (myInterlocutor == null) { return; }
        if (ElfData.canTalk())
        {
            Debug.Log("TALKING TO ELF");
            myElfAnimation.AcctionAnimation("Talk");
            StartCoroutine(TalkSequence());
        }
    }

    public void HavingCoffee()
    {
        if (myVendingMachine == null) { return; }
        Debug.Log("DRINKING COFFEE");
        myElfAnimation.AcctionAnimation("Coffee");
        StartCoroutine(CoffeeSequence());
    }

    private IEnumerator WrapSequence()
    {
        yield return new WaitForSeconds(myElfAnimation.AcctionDelay);
        //myCalloutManager.EnableCallout(false);
    }

    private IEnumerator TalkSequence()
    {
        myCalloutManager.SetCalloutSprite(CalloutTypes.Talk);
        myCalloutManager.EnableCallout(true);
        //ACA EFECTOS DE CHARLA
        float secondsPerResistence = myElfAnimation.AcctionDelay / ElfData.ResistenceToTalk;
        for (int i = 0; i < ElfData.ResistenceToTalk; i++)
        {
            yield return new WaitForSeconds(secondsPerResistence);
            ElfData.TalkTime++;
            Debug.Log("SEGUNDO DE CHARLA " + ElfData.TalkTime);
        }
        ElfData.TalkTime = 0;
        Debug.Log("RESET TALK");
        myCalloutManager.EnableCallout(false);
        StressManager.DecreaseStress(2);
    }

    private IEnumerator CoffeeSequence()
    {
        //myCalloutManager.SetCalloutSprite(CalloutTypes.Coffee);
        //myCalloutManager.EnableCallout(true);
        yield return new WaitForSeconds(myElfAnimation.AcctionDelay);
        StressManager.DecreaseStress(2);
        myVendingMachine = null;
        //myCalloutManager.EnableCallout(false);
    }

    public void MoveElf(float xDirection, float yDirection)
    {
        float xMove = GetMoveValue(xDirection);
        float yMove = GetMoveValue(yDirection);

        transform.Translate(xMove, yMove, 0f);
        myElfAnimation.Walking(xMove, yMove);
    }

    private float GetMoveValue(float direction)
    {
        return direction * elfData.WalkSpeed * Time.deltaTime;
    }

}
