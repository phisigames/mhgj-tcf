using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndpointManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Toys"))
        {
            Debug.Log("TOY IN AREA");
            other.gameObject.GetComponent<ToyMovement>().CanMove = false;
        }

        if (other.CompareTag("Player"))
        {
            Debug.Log("PLAYER IN AREA");
        }
    }
}
