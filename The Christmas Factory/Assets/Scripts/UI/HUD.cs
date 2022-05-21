using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    [SerializeField]
    private Slider stressBar;

    [SerializeField]
    private TextMeshProUGUI giftConfigLabel;

    [SerializeField]
    private GameObject statusPanel;

    [SerializeField]
    private GameObject tutorialPanel;

    [SerializeField]
    private TextMeshProUGUI statusPanelTitle;

    [SerializeField]
    private TextMeshProUGUI statusPanelSubtitle;

    [SerializeField]
    private TextMeshProUGUI statusGiftValue;

    [SerializeField]
    private TextMeshProUGUI statusLicenseValue;

    [SerializeField]
    private TextMeshProUGUI statusDistressValue;

    private void Awake()
    {

    }

    private void OnEnable()
    {
        StressManager.OnChangeStress += UpdateStressBar;
        GameManager.InCondition += OpenStatusPanel;
    }

    private void Start()
    {
        stressBar.value = StressManager.Instance.CumulativeStress;
        stressBar.maxValue = StressManager.Instance.StressCapacity;
    }

    private void Update()
    {
        giftConfigLabel.text = GameManager.Instance.GetGiftCount().ToString();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("PANEL STATUS");
            UpdateStatistics();
            statusPanel.SetActive(!statusPanel.activeSelf);

        }
    }

    public void UpdateStressBar()
    {
        stressBar.value = StressManager.Instance.CumulativeStress;
    }

    public void OpenStatusPanel(bool isWin)
    {
        statusPanelTitle.text = isWin ? "GIFT GOAL MET" : "GAME OVER";
        statusPanelSubtitle.text = isWin ? "Your workshop save Christmas" : "You need stress leave";
        UpdateStatistics();
        statusPanel.SetActive(true);
    }

    public void ClosePanel(GameObject panelObject)
    {
        panelObject.SetActive(false);
    }

    public void GoLanding()
    {
        GameManager.Instance.DestroyGameManager();
        SceneManager.LoadScene("Landing");
    }

    public void GoReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoTutorial()
    {
        tutorialPanel.SetActive(true);
    }

    private void UpdateStatistics()
    {
        statusGiftValue.text = "x" + GameManager.Instance.CumulativeGifts.ToString();
        statusLicenseValue.text = "x" + GameManager.Instance.CumulativeLicenses.ToString();
        statusDistressValue.text = "x" + GameManager.Instance.DistressHits.ToString();
    }

    private void OnDisable()
    {
        StressManager.OnChangeStress -= UpdateStressBar;
        GameManager.InCondition -= OpenStatusPanel;
    }

}
