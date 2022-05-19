using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    [SerializeField]
    private int cumulativeGifts;
    public int CumulativeGifts { get { return cumulativeGifts; } set { cumulativeGifts = value; } }

    [SerializeField]
    private int cumulativeLicenses;
    public int CumulativeLicenses { get { return cumulativeLicenses; } set { cumulativeLicenses = value; } }

    [SerializeField]
    private int distressHits;
    public int DistressHits { get { return distressHits; } set { distressHits = value; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            cumulativeGifts = 0;
            cumulativeLicenses = 0;
            distressHits = 0;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
