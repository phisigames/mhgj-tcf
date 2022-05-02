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


    // Update is called once per frame
    void Update()
    {
        MoveToy();
    }

    private void MoveToy()
    {
        if (!canMove) { return; }
        transform.Translate(Vector3.right * toySpeed * Time.deltaTime);
    }
}
