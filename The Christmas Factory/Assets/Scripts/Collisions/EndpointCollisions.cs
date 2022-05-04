using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndpointCollisions : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Toys"))
        {
            other.gameObject.SetActive(false);
        }
    }

}
