using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupervisorIA : MonoBehaviour
{
    [SerializeField] List<GameObject> path = new List<GameObject>();

    [SerializeField]
    [Range(0f, 5f)]
    private float speed = 1f;

    [SerializeField]
    private CalloutManager myCalloutManager = null;
    public CalloutManager MyCalloutManager { get { return myCalloutManager; } }

    [SerializeField]
    [Range(10, 90)]
    private int goodOrdenProbability = 50;
    public int GoodOrdenProbability { get { return goodOrdenProbability; } set { goodOrdenProbability = value; } }

    [SerializeField]
    [Range(0.1f, 5f)]
    private float delayBetweenOrders = 1f;
    public float DelayBetweenOrders { get { return delayBetweenOrders; } set { delayBetweenOrders = value; } }

    [SerializeField]
    [Range(0.5f, 10f)]
    private float delayBetweenRounds = 2f;
    public float DelayBetweenRounds { get { return delayBetweenRounds; } set { delayBetweenRounds = value; } }

    [SerializeField]
    [Range(1, 30)]
    private int emotionalDamage = 2;
    public int EmotionalDamage { get { return emotionalDamage; } set { emotionalDamage = value; } }

    [SerializeField]
    private SupervisorAnimationController mySupervisorAnimation = null;

    private void Awake()
    {
        mySupervisorAnimation = GetComponent<SupervisorAnimationController>();
    }

    void Start()
    {
        StartCoroutine(MakingRounds());
    }

    IEnumerator MakingRounds()
    {
        while (true)
        {
            for (int i = 0; i < path.Count; i++)
            {
                Vector3 startPosition = transform.position;
                Vector3 endPosition = path[i].transform.position;
                Vector3 direction  = endPosition - startPosition;
                mySupervisorAnimation.FlipSprite(direction.x);
                float travelPercent = 0f;
                while (travelPercent < 1f)
                {
                    travelPercent += Time.deltaTime * speed;
                    transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                    yield return new WaitForEndOfFrame();
                }

                if ((i + 1) < path.Count)
                {
                    if (rngRequest())
                    {
                        mySupervisorAnimation.AcctionAnimation("Good");
                        CalloutOrder(true);
                    }
                    else
                    {
                        mySupervisorAnimation.AcctionAnimation("Bad");
                        CalloutOrder(false);
                    }
                    yield return new WaitForSeconds(delayBetweenOrders);
                    mySupervisorAnimation.AcctionAnimation("Walk");
                    myCalloutManager.EnableCallout(false);
                }
            }
            yield return new WaitForSeconds(delayBetweenRounds);
        }
    }

    private void CalloutOrder(bool isGood)
    {
        if (isGood)
        {
            myCalloutManager.SetCalloutSprite(CalloutTypes.Good);
            myCalloutManager.EnableCallout(true);
            StressManager.DecreaseStress(emotionalDamage);
        }
        else
        {
            myCalloutManager.SetCalloutSprite(CalloutTypes.Bad);
            myCalloutManager.EnableCallout(true);
            StressManager.IncreaseStress(emotionalDamage);
        }
    }

    public bool rngRequest()
    {
        int random = Random.Range(1, 101);
        return random < goodOrdenProbability;
    }
}
