using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehavior : MonoBehaviour
{
    [SerializeField]
    public GameObject pauseMenu;
    public GameObject menuCraft;
    public static bool isPaused = false;
    public static bool isCraft = false;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            if (isCraft)
            {
                HideCraftMenu();

            }
            else
            {
                DisplayCraftMenu();
            }
        }
    }

    public void HideCraftMenu()
    {
        menuCraft.SetActive(false);
        isCraft = false;
    }
    public void DisplayCraftMenu()
    {
        menuCraft.SetActive(true);
        isCraft = true;
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void startGame()
    {
        SceneManager.LoadScene(1);
    }
    public void exitGame()
    {
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

}
