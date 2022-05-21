using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


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

    //GAME OVER CONDITION
    private bool isGameOver = false;

    [SerializeField]
    [Range(3, 10)]
    private float gameOverTimeGap = 3f;

    //EVENTS
    public static event Action<bool> InCondition;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }



    private void Start()
    {
        ResetStatistics();
    }

    private void Update()
    {
        if (IsWinCondition())
        {
            Debug.Log("WIN");
            PauseGame();
            InCondition?.Invoke(true);
        }

        if (IsLoseCondition() && !isGameOver)
        {
            isGameOver = true;
            Debug.Log("GAME OVER");
            Invoke("GameOver", gameOverTimeGap);
        }
    }

    public void ResetStatistics()
    {
        instance.cumulativeGifts = 0;
        instance.cumulativeLicenses = 0;
        instance.distressHits = 0;
    }

    public float GetGiftCount()
    {
        return DifficultyManager.Instance.GoalGift - cumulativeGifts;
    }

    public bool IsWinCondition()
    {
        return GetGiftCount() <= 0;
    }

    public bool IsLoseCondition()
    {
        return StressManager.Instance.IsMaxStressCapacity();
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        //pausePanel.SetActive(true);
        //Disable scripts that still work while timescale is set to 0
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        //pausePanel.SetActive(false);
        //enable the scripts again
    }

    public void DestroyGameManager()
    {
        Destroy(gameObject);
    }

    private void GameOver()
    {
        if (IsLoseCondition())
        {
            Debug.Log("LOSE");
            PauseGame();
            InCondition?.Invoke(false);
        }
        else
        {
            Debug.Log("STRESS LOW");
            isGameOver = false;
        }
    }
}
