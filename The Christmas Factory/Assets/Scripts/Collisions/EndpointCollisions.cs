using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndpointCollisions : MonoBehaviour
{

    [SerializeField]
    private int endpointGap = 1;
    public int EndpointGap { get { return endpointGap; } }

    [SerializeField]
    [Range(0.1f, 5f)]
    private float minPosition = 1;

    [SerializeField]
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.localPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Toys"))
        {
            other.gameObject.GetComponent<ToyMovement>().CanMove = false;
            other.gameObject.transform.localPosition = new Vector3(transform.localPosition.x - endpointGap/2, transform.position.y, transform.position.z);
            if (transform.localPosition.x > minPosition)
            {
                transform.localPosition = new Vector3(transform.localPosition.x - endpointGap, transform.position.y, transform.position.z);
            }
        }
    }

}
