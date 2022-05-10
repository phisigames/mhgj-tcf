using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class WorkshopPPManager : MonoBehaviour
{
    [SerializeField]
    [Range(-100, 0)]
    private int minimalSaturation = -100;
    public int MinimalSaturation { get { return minimalSaturation; } }

    [SerializeField]
    [Range(0, 100)]
    private int maximalSaturation = 0;
    public int MaximalSaturation { get { return maximalSaturation; } }

    [SerializeField]
    [Range(1, 20)]
    private float saturationFactor = 10;
    public float SaturationFactor { get { return saturationFactor; } }

    [SerializeField]
    private ColorGrading myColorGrading;

    private void Awake()
    {
        GetComponent<PostProcessVolume>().profile.TryGetSettings(out myColorGrading);
        StressManager.InDistress += DecreaseSaturation;
        StressManager.InStress   += IncreaseSaturation;
    }

    public void DecreaseSaturation()
    {
        if (myColorGrading.saturation.value > minimalSaturation)
        {
            myColorGrading.saturation.value -= saturationFactor;
        }
    }

    public void IncreaseSaturation()
    {
        if (myColorGrading.saturation.value < maximalSaturation)
        {
            myColorGrading.saturation.value += saturationFactor;
        }
    }
}
