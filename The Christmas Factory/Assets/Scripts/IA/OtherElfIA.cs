using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherElfIA : MonoBehaviour
{
    [SerializeField] List<Transform> path = new List<Transform>();

    [SerializeField]
    private ElfActionsManager myActionsManager = null;

    [SerializeField]
    private bool isWorking = true;
    public bool IsWorking { get { return isWorking; } }

    [SerializeField]
    private float breakGap = 1f;

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

        if (myActionsManager.MyConveyor.IsInBreak && isWorking)
        {
            Debug.Log("IN BREAK");
            isWorking = false;
            StartCoroutine(BreakBehaviour());
        }
    }

    private IEnumerator BreakBehaviour()
    {
        float myBreakTime = myActionsManager.MyConveyor.BreakTime - breakGap;
        for (int i = 0; i < path.Count; i++)
        {
            Debug.Log("PONT: " + i);
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
}
