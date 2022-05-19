using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputElfManager : MonoBehaviour
{
    [SerializeField]
    private ElfActionsManager myActionsManager = null;

    void Start()
    {
        myActionsManager = GetComponent<ElfActionsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //MOVE INPUT
        if (!myActionsManager.MyElfAnimation.InAction)
        {
            //FIX IDLE-> SEARCH OTHER SOLUTION
            if (Input.GetKeyDown(KeyCode.W) ||
                Input.GetKeyDown(KeyCode.D) ||
                Input.GetKeyDown(KeyCode.A) ||
                Input.GetKeyDown(KeyCode.S) ||
                Input.GetKeyDown(KeyCode.UpArrow) ||
                Input.GetKeyDown(KeyCode.RightArrow) ||
                Input.GetKeyDown(KeyCode.LeftArrow) ||
                Input.GetKeyDown(KeyCode.DownArrow))
            {
                myActionsManager.MyElfAnimation.SlideIdle();
            }


        }

        //INPUT SPACE
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myActionsManager.ToyWrapping();
            myActionsManager.Talk();
            myActionsManager.HavingCoffee();
        }
    }

    private void FixedUpdate()
    {
        if (!myActionsManager.MyElfAnimation.InAction)
        {
            myActionsManager.MoveElf(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
    }
}
