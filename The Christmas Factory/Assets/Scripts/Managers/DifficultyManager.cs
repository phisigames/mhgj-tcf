using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [SerializeField] private static DifficultyManager instance;
    public static DifficultyManager Instance { get { return instance; } }

    [SerializeField]
    private float goalGift;
    public float GoalGift { get { return goalGift; } set { goalGift = value; } }

    [SerializeField]
    private float stressPlayer;
    public float StressPlayer { get { return stressPlayer; } set { stressPlayer = value; } }

    [SerializeField]
    private float stressNPC;
    public float StressNPC { get { return stressNPC; } set { stressNPC = value; } }

    [SerializeField]
    private float workingScale;
    public float WorkingScale { get { return workingScale; } set { workingScale = value; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            goalGift = 100;
            stressPlayer = 20;
            stressNPC = 16;
            workingScale = 40;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
