using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StressManager : MonoBehaviour
{
    [SerializeField]
    [Range(10, 100)]
    private int stressCapacity = 20;
    public int StressCapacity { get { return stressCapacity; } }

    [SerializeField]
    private int cumulativeStress = 0;
    public int CumulativeStress { get { return cumulativeStress; } set { cumulativeStress = value; } }

    [SerializeField] private static StressManager instance;
    public static StressManager Instance { get { return instance; } }

    //EVENTS
    public static event Action OnChangeStress;
    public static event Action InDistress;
    public static event Action InStress;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            cumulativeStress = 0;
            stressCapacity = (int) DifficultyManager.Instance.StressPlayer;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void DecreaseStress(int value)
    {
        instance.cumulativeStress -= value;

        if (instance.cumulativeStress < 0)
        {
            instance.cumulativeStress = 0;
        }

        InStress?.Invoke();
        OnChangeStress?.Invoke();
    }

    public static void IncreaseStress(int value)
    {
        instance.cumulativeStress += value;

        if (instance.cumulativeStress > instance.stressCapacity)
        {
            instance.cumulativeStress = instance.stressCapacity;
        }

        if (instance.IsInDistress())
        {
            InDistress?.Invoke();
            GameManager.Instance.DistressHits++;
        }

        OnChangeStress?.Invoke();
    }

    private bool IsInDistress()
    {
        return (instance.cumulativeStress >= (instance.stressCapacity / 2));
    }

    public bool IsMaxStressCapacity()
    {
        return instance.cumulativeStress == instance.stressCapacity;
    }
}
