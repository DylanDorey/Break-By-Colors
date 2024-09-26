using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mainmenu : MonoBehaviour
{
    public static bool GameMenu = false;
    public GameObject mainMenuUI;
    public GameObject QuitButton;
    public GameObject RestartButton;
    public TextMeshProUGUI Play;
    public bool MenuUp = false;

    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameMenu)
            {
                Resume();
            }
            else
            {
                ActiveMenu();
            }
        }
    }

    public void Resume()
    {
        mainMenuUI.SetActive(false);
        QuitButton.SetActive(false);
        RestartButton.SetActive(false);
        MenuUp = false;
        Time.timeScale = 1f;
        GameMenu = false;
    }

    void ActiveMenu()
    {
        mainMenuUI.SetActive(true);
        QuitButton.SetActive(true);
        RestartButton.SetActive(true);
        MenuUp = true;
        Time.timeScale = 0f;
        GameMenu = true;
        Play.text = "Play";
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
