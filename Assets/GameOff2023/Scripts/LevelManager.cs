using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static LevelManager levelManager;

    [SerializeField]
    private GameObject loadingScreen;

    private string currentLevel;
    // Start is called before the first frame update
    void Start()
    {
        if(levelManager != null)
        {
            Destroy(this);
        }
        else
        {
            levelManager = this;
        }

        DontDestroyOnLoad(this);

        //on start the loading screen shouldnt be active, only when loading new scenes
        loadingScreen.SetActive(false);
    }

    public void LoadLevel(string levelName)
    {
        StartCoroutine(LoadSceneAsync(levelName));
        currentLevel = levelName;
    }

    public void RestartLevel()
    {
        LoadLevel(currentLevel);
    }

    private IEnumerator LoadSceneAsync(string levelName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelName);

        loadingScreen.SetActive(true);
        while(!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress/0.9f);

            //Do something to show game is loading
            yield return null;
        }

        //Close the loading screen again when new scene has been loaded
        loadingScreen.SetActive(false);
    }
}
