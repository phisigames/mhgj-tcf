using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherElfIA : MonoBehaviour
{
    [SerializeField] List<Transform> path = new List<Transform>();

    [SerializeField] List<Transform> pathLicense = new List<Transform>();

    [SerializeField]
    private ElfActionsManager myActionsManager = null;

    [SerializeField]
    private bool isWorking = true;
    public bool IsWorking { get { return isWorking; } }

    [SerializeField]
    private float breakGap = 1f;

    [SerializeField]
    private bool isInLicense;

    [SerializeField]
    private float licenseTime = 8f;


    void Start()
    {
        myActionsManager = GetComponent<ElfActionsManager>();
        SetStressCapacity((int)DifficultyManager.Instance.StressNPC);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInLicense)
        {
            if (!myActionsManager.ElfData.isLimitToStress())
            {
                if (!myActionsManager.MyElfAnimation.InAction)
                {
                    myActionsManager.ToyWrapping();
                }

                if (myActionsManager.MyConveyor.IsInBreak && isWorking)
                {
                    isWorking = false;
                    StartCoroutine(BreakBehaviour());
                }
            }
        }

        if (myActionsManager.ElfData.isLimitToStress() && !isInLicense)
        {
            GameManager.Instance.CumulativeLicenses++;
            isInLicense = true;
            myActionsManager.MyConveyor.TerminalOff();
            StartCoroutine(LicenseBehaviour());
        }

    }

    private IEnumerator BreakBehaviour()
    {
        float myBreakTime = myActionsManager.MyConveyor.BreakTime - breakGap;
        for (int i = 0; i < path.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = path[i].transform.position;
            Vector3 direction = endPosition - startPosition;
            myActionsManager.MyElfAnimation.SlideIdle();
            myActionsManager.MyElfAnimation.Walking(direction.x, direction.y);
            float travelPercent = 0f;
            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * myActionsManager.ElfData.WalkSpeed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }

            myActionsManager.MyElfAnimation.WalkingAnimator(false);
            myActionsManager.MyElfAnimation.FrontIdle();

            if ((i + 1) <= path.Count)
            {
                yield return new WaitForSeconds(myBreakTime);
            }
        }
        isWorking = true;
    }


    private IEnumerator LicenseBehaviour()
    {
        for (int i = 0; i < pathLicense.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = pathLicense[i].transform.position;
            Vector3 direction = endPosition - startPosition;
            myActionsManager.MyElfAnimation.SlideIdle();
            myActionsManager.MyElfAnimation.Walking(direction.x, direction.y);
            float travelPercent = 0f;
            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * 0.7f;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }

            myActionsManager.MyElfAnimation.WalkingAnimator(false);
            myActionsManager.MyElfAnimation.FrontIdle();

            if ((i + 1) <= path.Count)
            {
                yield return new WaitForSeconds(licenseTime);
            }
        }

        myActionsManager.ElfData.CumulativeStress = 0;
        isInLicense = false;
        isWorking = true;
        myActionsManager.MyConveyor.TerminalOn();
    }

    public bool ElfResponse()
    {
        myActionsManager.MyElfAnimation.AcctionAnimation("Talk");
        Invoke("HideCallout", myActionsManager.MyElfAnimation.AcctionDelay);
        if (myActionsManager.MyConveyor.IsInBreak)
        {
            myActionsManager.MyCalloutManager.SetCalloutSprite(CalloutTypes.Talk);
            myActionsManager.MyCalloutManager.EnableCallout(true);
            myActionsManager.ElfData.DecreaseStress(myActionsManager.TalkEmotionalDamage);
            return true;
        }
        else
        {
            myActionsManager.MyCalloutManager.SetCalloutSprite(CalloutTypes.Angry);
            myActionsManager.MyCalloutManager.EnableCallout(true);
            myActionsManager.ElfData.IncreaseStress(myActionsManager.TalkEmotionalDamage);
            return false;
        }

    }

    private void HideCallout()
    {
        myActionsManager.MyCalloutManager.EnableCallout(false);
    }

    public void SetStressCapacity(int capacity)
    {
        myActionsManager.ElfData.StressCapacity = capacity;
    }
}
