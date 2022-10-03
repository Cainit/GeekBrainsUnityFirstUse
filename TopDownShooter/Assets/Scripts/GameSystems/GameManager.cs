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
    [SerializeField] GameObject messageBox;
    [SerializeField]
    TMPro.TextMeshProUGUI messageBoxCaption;
    [SerializeField]
    TMPro.TextMeshProUGUI messageBoxText;

    void Awake()
    {
        loadingScreen.SetActive(false);
        resumeGameButton.SetActive(false);
        messageBox.SetActive(false);

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

    public void NewGame()
    {
        newGameButton.gameObject.SetActive(false);
        resumeGameButton.SetActive(true);
        mainMenu.SetActive(false);

        LoadScene("Level1");

        gameOn = true;
    }

    public async void LoadScene(string sceneName)
    {
        loadingScreen.SetActive(true);
        loadingProgress.transform.localScale = new Vector3(0, 1, 1);

        var newScene = SceneManager.LoadSceneAsync(sceneName);
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

            if (messageBox.activeSelf)
            {
                HideMessage();
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

    public void ShowMessage(string caption, string text)
    {
        messageBox.SetActive(true);
        
        messageBoxCaption.text = caption;
        messageBoxText.text = text;

        Time.timeScale = 0;
    }

    public void HideMessage()
    {
        messageBox.SetActive(false);
        Time.timeScale = 1;
    }
}
