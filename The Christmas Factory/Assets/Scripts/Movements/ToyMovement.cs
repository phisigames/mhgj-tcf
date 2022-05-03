using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyMovement : MonoBehaviour
{
    [SerializeField]
    [Range(5, 50)]
    private float toySpeed = 2;

    [SerializeField] private bool canMove = true;
    public bool CanMove { get { return canMove; } set { canMove = value; } }

    [SerializeField]
    private Vector3 initialPosition;

    private void Awake()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        MoveToy();
    }

    private void MoveToy()
    {
        if (!canMove) { return; }
        transform.Translate(Vector3.right * toySpeed * Time.deltaTime);
    }

    private void OnDisable()
    {
        transform.position = initialPosition;
        canMove = true;
    }
}
