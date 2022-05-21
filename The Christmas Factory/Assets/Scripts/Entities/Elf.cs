using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elf : MonoBehaviour
{
    //DESING DATA
    [SerializeField]
    [Range(1, 10)]
    private int resistenceToWrap = 3;
    public int ResistenceToWrap { get { return resistenceToWrap; } set { resistenceToWrap = value; } }

    [SerializeField]
    [Range(1, 10)]
    private int resistenceToTalk = 3;
    public int ResistenceToTalk { get { return resistenceToTalk; } set { resistenceToTalk = value; } }

    [SerializeField]
    [Range(1, 50)]
    private float walkSpeed = 5;
    public float WalkSpeed { get { return walkSpeed; } set { walkSpeed = value; } }

    //INDIVIDUAL STRESS MANAGMENT (CODE NEEDED FOR FUTURE  CO-OP FUNCTION)
    [SerializeField]
    [Range(10, 100)]
    private int stressCapacity = 15;
    public int StressCapacity { get { return stressCapacity; } set { stressCapacity = value; }}

    [SerializeField]
    private int cumulativeStress = 0;
    public int CumulativeStress { get { return cumulativeStress; } set { cumulativeStress = value; } }

    [SerializeField]
    private bool isNPC = false;
    public bool IsNPC { get { return isNPC; } }
    //------------------------------------------------------------------------------------------------------
    //RUNTIME DATA
    [SerializeField]
    private int giftWrapping = 0;
    public int GiftWrapping { get { return giftWrapping; } set { giftWrapping = value; } }

    //RUNTIME DATA
    [SerializeField]
    private int talkTime = 0;
    public int TalkTime { get { return talkTime; } set { talkTime = value; } }

    public bool isLimitToWrap()
    {
        return giftWrapping == resistenceToWrap;
    }

    public bool isLimitToTalk()
    {
        return talkTime == resistenceToTalk;
    }

    public bool canTalk()
    {
        return talkTime == 0;
    }

    public void DecreaseStress(int value)
    {
        cumulativeStress -= value;

        if (cumulativeStress < 0)
        {
            cumulativeStress = 0;
        }
    }

    public void IncreaseStress(int value)
    {
        cumulativeStress += value;

        if (cumulativeStress > stressCapacity)
        {
            cumulativeStress = stressCapacity;
        }
    }

    public bool isLimitToStress()
    {
        return cumulativeStress == stressCapacity;
    }
}
