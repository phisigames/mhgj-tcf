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
    [Range(1, 10)]
    private float saturationDecrease = 2;
    public float SaturationDecrease { get { return saturationDecrease; } }

    [SerializeField]
    private ColorGrading myColorGrading;

    private void Awake()
    {
        GetComponent<PostProcessVolume>().profile.TryGetSettings(out myColorGrading);
    }

    public void DecreaseSaturation()
    {
        if (myColorGrading.saturation.value > minimalSaturation)
        {
            myColorGrading.saturation.value -= saturationDecrease;
        }
    }
}
