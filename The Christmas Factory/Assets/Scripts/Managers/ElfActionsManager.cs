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

    //------------- DAMAGE ACTION ------------- 
    [SerializeField]
    [Range(1, 30)]
    private int talkEmotionalDamage = 2;
    public int TalkEmotionalDamage { get { return talkEmotionalDamage; } set { talkEmotionalDamage = value; } }

    [SerializeField]
    [Range(1, 30)]
    private int wrapEmotionalDamage = 1;
    public int WrapEmotionalDamage { get { return wrapEmotionalDamage; } set { wrapEmotionalDamage = value; } }

    [SerializeField]
    [Range(1, 30)]
    private int coffeeEmotionalDamage = 2;
    public int CoffeeEmotionalDamage { get { return coffeeEmotionalDamage; } set { coffeeEmotionalDamage = value; } }

    private void Awake()
    {
        myElfAnimation = GetComponent<ElfAnimationController>();
        elfData = GetComponent<Elf>();
        myElfRigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void ToyWrapping()
    {
        if (nextToy == null) { return; }

        myElfAnimation.AcctionAnimation("Wrap");
        ElfData.GiftWrapping++;
        GameManager.Instance.CumulativeGifts++;
        if (ElfData.isLimitToWrap())
        {
            ElfData.GiftWrapping = 0;
            ElfData.IncreaseStress(wrapEmotionalDamage);
            if (!ElfData.IsNPC)
            {
                StressManager.IncreaseStress(wrapEmotionalDamage);
            }
            myCalloutManager.SetCalloutSprite(CalloutTypes.Bad);
            myCalloutManager.EnableCallout(true);
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
        myCalloutManager.EnableCallout(false);
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
                StressManager.DecreaseStress(talkEmotionalDamage);
            }
            else
            {
                StressManager.IncreaseStress(talkEmotionalDamage);
            }
        }
    }

    private IEnumerator CoffeeSequence()
    {
        myCalloutManager.SetCalloutSprite(CalloutTypes.Good);
        myCalloutManager.EnableCallout(true);
        yield return new WaitForSeconds(myElfAnimation.AcctionDelay);
        StressManager.DecreaseStress(CoffeeEmotionalDamage);
        myVendingMachine = null;
        myCalloutManager.EnableCallout(false);
    }

    public void MoveElf(float xPosition, float yPosition)
    {
        Vector2 direction = new Vector2(xPosition, yPosition);
        myElfRigidbody2D.MovePosition((Vector2)transform.position + direction * Time.deltaTime * elfData.WalkSpeed);
        myElfAnimation.Walking(xPosition, yPosition);
    }
}
