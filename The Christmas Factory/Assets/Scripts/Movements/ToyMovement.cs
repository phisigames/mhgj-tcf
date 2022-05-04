using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyMovement : MonoBehaviour
{
    [SerializeField]
    private Vector3 initialPosition;

    private void Awake()
    {
        initialPosition = transform.position;
    }

    private void OnDisable()
    {
        transform.position = initialPosition;
        transform.rotation = Quaternion.identity;
    }
}
