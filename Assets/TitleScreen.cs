using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TitleScreen : MonoBehaviour
{

    public GameObject MenuScreen;
    public GameObject HelpScreen;

    void Awake() {
        PauseMenu.GameIsPaused = false;
    }
    // load the scene for the game
    public void PlayGame() {
        PauseMenu.GameIsPaused = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    public void LoadMenu() {
        PauseMenu.GameIsPaused = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScreen");
    }

    public void ShowMenu() {
        MenuScreen.SetActive(true);
        HelpScreen.SetActive(false);
    }

    public void ShowHelp() {
        MenuScreen.SetActive(false);
        HelpScreen.SetActive(true);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
