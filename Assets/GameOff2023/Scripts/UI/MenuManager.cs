using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] allPanels;
    [SerializeField]
    public GameObject startPanel;
    [HideInInspector]
    public GameObject currentPanel;
    // Start is called before the first frame update
    void Start()
    {
        SwitchPanel(startPanel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchPanel(GameObject panelToSwitchTo)
    {
        foreach(GameObject panel in allPanels)
        {
            panel.SetActive(false);
        }
        panelToSwitchTo.SetActive(true);
        currentPanel = panelToSwitchTo;
    }

    public void OpenLevel(string levelToOpen)
    {
        if (LevelManager.levelManager == null)
        {
            Debug.LogError("No level manager instance exists, not doing anything");
        }
        LevelManager.levelManager.LoadLevel(levelToOpen);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
