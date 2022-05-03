using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndpointCollisions : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 5f)]
    private float endpointGap = 1.2f;

    [SerializeField]
    [Range(0.1f, 5f)]
    private float minPosition = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Toys"))
        {
            Debug.Log("TOY IN ENDPOINT");
            other.gameObject.GetComponent<ToyMovement>().CanMove = false;
            Debug.Log(transform.localPosition.x);
            if (transform.localPosition.x > minPosition)
            {
                transform.localPosition = new Vector3((transform.localPosition.x - endpointGap), transform.position.y, transform.position.z);
            }
        }
    }
}
