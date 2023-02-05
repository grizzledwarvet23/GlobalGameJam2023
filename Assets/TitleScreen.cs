using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TitleScreen : MonoBehaviour
{


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
}
