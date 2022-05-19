using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LandingManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
}
