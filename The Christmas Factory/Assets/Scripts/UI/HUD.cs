using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField]
    private Slider stressBar;

    void Start()
    {
        stressBar.value = StressManager.Instance.CumulativeStress;
        stressBar.maxValue = StressManager.Instance.StressCapacity;
    }

    public void UpdateStressBar()
    {
        stressBar.value = StressManager.Instance.CumulativeStress;
    }
}
