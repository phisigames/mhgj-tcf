using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorManager : MonoBehaviour
{

    [SerializeField]
    [Range(1, 10)]
    private int conveyorMaxCapacity = 7;
    public int ConveyorMaxCapacity { get { return conveyorMaxCapacity; } set { conveyorMaxCapacity = value; } }

    [SerializeField]
    private int conveyorCapacity = 0;
    public int ConveyorCapacity { get { return conveyorCapacity; } set { conveyorCapacity = value; } }

    [SerializeField]
    private ToySpawner myToySpawner;

    [SerializeField]
    private ConveyorAnimationController myConveyorAnimation = null;


    // REAL CONFIG
    [SerializeField]
    private SurfaceEffector2D myRell = null;

    [SerializeField]
    [Range(1, 10)]
    private int speedReel = 3;
    public int SpeedReel { get { return speedReel; } set { speedReel = value; } }

    [SerializeField]
    [Range(1, 10)]
    private int speedVariationReel = 3;
    public int SpeedVariationReel { get { return speedVariationReel; } set { speedVariationReel = value; } }

    // BREAK CONFIG
    [SerializeField]
    [Range(1, 120)]
    private int breakTime = 10;
    public int BreakTime { get { return breakTime; } set { breakTime = value; } }

    [SerializeField]
    [Range(1, 120)]
    private int workTime = 10;
    public int WorkTime { get { return workTime; } set { workTime = value; } }

    [SerializeField]
    private bool isTerminalRun = true;

    [SerializeField]
    private bool isInBreak = false;
    public bool IsInBreak { get { return isInBreak; } set { isInBreak = value; } }

    private void Awake()
    {
        myToySpawner = GetComponent<ToySpawner>();
        myConveyorAnimation = GetComponent<ConveyorAnimationController>();
    }

    private void Start()
    {
        StartCoroutine(RunConveyorTermninal());
    }

    private IEnumerator RunConveyorTermninal()
    {
        while (isTerminalRun)
        {
            Debug.Log("WORK TIME");
            myConveyorAnimation.TriggerAnimation("On");
            myConveyorAnimation.TriggerReelAnimation("On");
            isInBreak = false;
            ResetReel(speedReel,speedVariationReel);
            yield return new WaitForSeconds(workTime);
            Debug.Log("BREAK TIME");
            myConveyorAnimation.TriggerAnimation("Off");
            myConveyorAnimation.TriggerReelAnimation("Off");
            isInBreak = true;
            ResetReel(0,0);
            yield return new WaitForSeconds(breakTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<ElfActionsManager>().MyConveyor = this;
        }

        if (other.CompareTag("Toys"))
        {
            conveyorCapacity++;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<ElfActionsManager>().MyConveyor = null;
        }

        if (other.CompareTag("Toys"))
        {
            conveyorCapacity--;
            EnableToySpawner();
        }
    }

    private void EnableToySpawner()
    {

        if ((conveyorCapacity < conveyorMaxCapacity) && !myToySpawner.enabled)
        {
            GetComponent<ToySpawner>().enabled = true;
        }
    }

    private ToyMovement[] GetToysInConveyor()
    {
        return GetComponentsInChildren<ToyMovement>();
    }

    private void ResetReel(int speed, int speedVarition)
    {
        myRell.speed = speed;
        myRell.speedVariation = speedVarition;
    }
}
