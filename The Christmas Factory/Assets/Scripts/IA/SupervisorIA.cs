using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupervisorIA : MonoBehaviour
{
    [SerializeField] List<GameObject> path = new List<GameObject>();

    [SerializeField]
    [Range(0f, 5f)]
    private float speed = 1f;

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
                    StressManager.Instance.CumulativeStress--;
                }
                else
                {
                    StressManager.Instance.CumulativeStress++;
                    Debug.Log("BAD ORDER");
                }
                //REMPLACE WITH EVENT SOLUTION
                FindObjectOfType<HUD>().UpdateStressBar();
                yield return new WaitForSeconds(1f);

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
