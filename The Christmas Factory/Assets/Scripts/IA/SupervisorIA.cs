using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupervisorIA : MonoBehaviour
{
    [SerializeField] List<GameObject> path = new List<GameObject>();

    [SerializeField]
    [Range(0f, 5f)]
    private float speed = 1f;

    
    [SerializeField] private CalloutManager myCalloutManager = null;
    public CalloutManager MyCalloutManager { get { return myCalloutManager; }}

    void Start()
    {
        StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath()
    {
        while (true)
        {
            foreach (GameObject waypoint in path)
            {
                Vector3 startPosition = transform.position;
                Vector3 endPosition = waypoint.transform.position;
                float travelPercent = 0f;
                //transform.LookAt(endPosition);
                while (travelPercent < 1f)
                {
                    travelPercent += Time.deltaTime * speed;
                    transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                    yield return new WaitForEndOfFrame();
                }
                if (rngRequest())
                {
                    Debug.Log("GOOD ORDER");
                    myCalloutManager.SetCalloutSprite(CalloutTypes.Good);
                    myCalloutManager.EnableCallout(true);
                    StressManager.Instance.CumulativeStress--;
                }
                else
                {
                    myCalloutManager.SetCalloutSprite(CalloutTypes.Bad);
                    myCalloutManager.EnableCallout(true);
                    StressManager.Instance.CumulativeStress++;
                    Debug.Log("BAD ORDER");
                }
                //REMPLACE WITH EVENT SOLUTION
                FindObjectOfType<HUD>().UpdateStressBar();
                yield return new WaitForSeconds(1f);
                myCalloutManager.EnableCallout(false);
            }
            yield return new WaitForSeconds(2f);
        }
    }

    public bool rngRequest()
    {
        int random = Random.Range(1, 101);
        Debug.Log(random);
        return random < 50;
    }
}
