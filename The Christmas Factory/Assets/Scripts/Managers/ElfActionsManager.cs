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

    [SerializeField]
    private Rigidbody2D myElfRigidbody2D;
    public Rigidbody2D MyElfRigidbody2D { get { return myElfRigidbody2D; } }

    private void Awake()
    {
        myElfAnimation = GetComponent<ElfAnimationController>();
        elfData = GetComponent<Elf>();
        myElfRigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void ToyWrapping()
    {
        if (nextToy == null) { return; }
        //Debug.Log("WRAPPING TOY");
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
            myElfAnimation.AcctionAnimation("Talk");
            StartCoroutine(TalkSequence());
        }
    }

    public void HavingCoffee()
    {
        if (myVendingMachine == null) { return; }
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
        }
        ElfData.TalkTime = 0;
        myCalloutManager.EnableCallout(false);
        if (myInterlocutor != null)
        {
            bool response = myInterlocutor.GetComponent<OtherElfIA>().ElfResponse();
            if (response)
            {
                StressManager.DecreaseStress(2);
            }
            else
            {
                StressManager.IncreaseStress(2);
            }

        }
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

    public void MoveElf(float xPosition, float yPosition)
    {
        Vector2 direction = new Vector2(xPosition, yPosition);
        myElfRigidbody2D.MovePosition((Vector2)transform.position + direction * Time.deltaTime * elfData.WalkSpeed);
        myElfAnimation.Walking(xPosition, yPosition);
    }
}
