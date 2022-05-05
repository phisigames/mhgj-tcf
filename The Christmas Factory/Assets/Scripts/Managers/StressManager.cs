using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            cumulativeStress = 0;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
