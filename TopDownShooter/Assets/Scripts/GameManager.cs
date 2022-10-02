using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    bool gameOn;
    bool paused;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject newGameButton;
    [SerializeField] GameObject resumeGameButton;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] GameObject loadingProgress;
    [SerializeField] GameObject optionsMenu;

    void Awake()
    {
        loadingScreen.SetActive(false);
        resumeGameButton.SetActive(false);

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public async void NewGame()
    {
        newGameButton.gameObject.SetActive(false);
        resumeGameButton.SetActive(true);
        mainMenu.SetActive(false);

        loadingScreen.SetActive(true);
        loadingProgress.transform.localScale = new Vector3(0, 1, 1);

        var newScene = SceneManager.LoadSceneAsync("Level1");
        newScene.allowSceneActivation = false;

        while (newScene.progress < 0.9f)
        {
            loadingProgress.transform.localScale = new Vector3(newScene.progress, 1, 1);
            await Task.Delay(10);
        }

        loadingProgress.transform.localScale = new Vector3(1, 1, 1);
        //for  screen
        await Task.Delay(1000);

        newScene.allowSceneActivation = true;

        loadingScreen.SetActive(false);

        gameOn = true;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && gameOn)
        {
            if(optionsMenu.activeSelf)
            {
                Options.Instance.Close();
                return;
            }

            if (paused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        paused = true;
        mainMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        paused = false;
        mainMenu.SetActive(false);
    }

    public void OpenOptions()
    {
        Options.Instance.Open();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
