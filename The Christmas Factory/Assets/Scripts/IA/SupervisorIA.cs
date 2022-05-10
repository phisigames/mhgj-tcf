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

    void Start()
    {
        StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath()
    {
        while (true)
        {
            for (int i = 0; i < path.Count; i++)
            {
                Vector3 startPosition = transform.position;
                Vector3 endPosition = path[i].transform.position;
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
                        Debug.Log("GOOD ORDER");
                        CalloutOrder(true);
                    }
                    else
                    {
                        Debug.Log("BAD ORDER");
                        CalloutOrder(false);
                    }
                    //REMPLACE WITH EVENT SOLUTION
                    FindObjectOfType<HUD>().UpdateStressBar();
                    yield return new WaitForSeconds(delayBetweenOrders);
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
            StressManager.Instance.CumulativeStress--;
        }
        else
        {
            myCalloutManager.SetCalloutSprite(CalloutTypes.Bad);
            myCalloutManager.EnableCallout(true);
            StressManager.Instance.CumulativeStress++;
        }
    }

    public bool rngRequest()
    {
        int random = Random.Range(1, 101);
        return random < goodOrdenProbability;
    }
}
