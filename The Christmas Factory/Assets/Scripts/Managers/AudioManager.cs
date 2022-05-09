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
    [Range(0.01f, 0.5f)]
    private float pitchDecrease = 0.01f;
    public float PitchDecrease { get { return pitchDecrease; } }

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
            myAudioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DecreasePitch()
    {
        if (myAudioSource.pitch > minimalPitch)
        {
            myAudioSource.pitch -= pitchDecrease;
        }
    }
}
