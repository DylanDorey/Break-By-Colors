using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Pause : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject QuitButton;
    public GameObject RestartButton;
    public TextMeshProUGUI Paused;
    public bool MenuUp = false;

    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                PauseMenu();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        QuitButton.SetActive(false);
        RestartButton.SetActive(false);
        MenuUp = false;
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void PauseMenu()
    {
        pauseMenuUI.SetActive(true);
        QuitButton.SetActive(true);
        RestartButton.SetActive(true);
        MenuUp = true;
        Time.timeScale = 0f;
        Debug.Log("bruh");
        GameIsPaused = true;
        Paused.text = "Paused";
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
