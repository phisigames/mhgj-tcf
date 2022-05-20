using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LandingManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI giftConfigLabel;

    [SerializeField]
    private TextMeshProUGUI maxPlayerConfigLabel;

    [SerializeField]
    private TextMeshProUGUI maxNPCConfigLabel;

    [SerializeField]
    private TextMeshProUGUI WorkConfigLabel;

    void Start()
    {
        UpdateAllDifficultyLabels();
    }

    public void ClosePanel(GameObject panelObject)
    {
        panelObject.SetActive(false);
    }

    public void OpenPanel(GameObject panelObject)
    {
        panelObject.SetActive(true);
    }

    public void GoWorkshop()
    {
        SceneManager.LoadScene("WorkshopA");
    }

    private void UpdateAllDifficultyLabels()
    {
        giftConfigLabel.text = DifficultyManager.Instance.GoalGift.ToString();
        maxPlayerConfigLabel.text = DifficultyManager.Instance.StressPlayer.ToString();
        maxNPCConfigLabel.text = DifficultyManager.Instance.StressNPC.ToString();
        WorkConfigLabel.text = DifficultyManager.Instance.WorkingScale.ToString();
    }

    public void UpdateGiftGoalDifficulty(float value)
    {
        DifficultyManager.Instance.GoalGift = value;
        giftConfigLabel.text = value.ToString();
    }

    public void UpdateStressPlayerDifficulty(float value)
    {
        DifficultyManager.Instance.StressPlayer = value;
        maxPlayerConfigLabel.text = value.ToString();
    }

    public void UpdateStressNPCDifficulty(float value)
    {
        DifficultyManager.Instance.StressNPC = value;
        maxNPCConfigLabel.text = value.ToString();
    }

    public void UpdateWorkingTimeDifficulty(float value)
    {
        DifficultyManager.Instance.WorkingScale = value;
        WorkConfigLabel.text = value.ToString();
    }

}
