using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndpointCollisions : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Toys"))
        {
            StressManager.Instance.CumulativeStress++;
            //REMPLACE WITH EVENT SOLUTION
            FindObjectOfType<HUD>().UpdateStressBar();
            AudioManager.Instance.DecreasePitch();
            other.gameObject.SetActive(false);
        }
    }

}
