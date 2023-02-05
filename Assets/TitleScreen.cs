using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TitleScreen : MonoBehaviour
{

    // load the scene for the game
    public void PlayGame() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
}
