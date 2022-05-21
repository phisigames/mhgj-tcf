using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 1f)]
    private float minimalPitch = 0.02f;
    public float MinimalPitch { get { return minimalPitch; } }

    [SerializeField]
    private float maximalPitch = 1f;
    public float MaximalPitch { get { return maximalPitch; } }

    [SerializeField]
    [Range(0.01f, 0.5f)]
    private float pitchFactor = 0.05f;
    public float PitchFactor { get { return pitchFactor; } }

    [SerializeField]
    private AudioSource myAudioSource;
    public AudioSource MyAudioSource { get { return myAudioSource; } }

    [SerializeField] private static AudioManager instance;
    public static AudioManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        StressManager.InDistress += DecreasePitch;
        StressManager.InStress += IncreasePitch;
    }

    public void DecreasePitch()
    {
        if (myAudioSource.pitch > minimalPitch)
        {
            myAudioSource.pitch -= pitchFactor;
        }
    }

    public void IncreasePitch()
    {
        if (myAudioSource.pitch < maximalPitch)
        {
            myAudioSource.pitch += pitchFactor;
        }
    }

    private void OnDisable() {
        StressManager.InDistress -= DecreasePitch;
        StressManager.InStress -= IncreasePitch;
    }
}
