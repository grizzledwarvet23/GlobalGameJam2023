using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinScreen : MonoBehaviour
{
    public Image blackFade;
    public TextMeshProUGUI winText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(winAnimation());       
    }

    IEnumerator winAnimation()
    {
        // make the black opacity gradually go to 0, to reveal the win screen. then, wait a few seconds, then increase opacity of win text to 1 to reveal the text. then, wait a few seconds, fade to black, then load the main menu.
        float opacity = 1f;
        while (opacity > 0)
        {
            opacity -= 0.01f;
            blackFade.color = new Color(0, 0, 0, opacity);
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(3f);
        opacity = 0f;
        while (opacity < 1)
        {
            opacity += 0.01f;
            winText.color = new Color(1, 1, 1, opacity);
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(5f);
        opacity = 0f;
        while (opacity < 1)
        {
            opacity += 0.01f;
            blackFade.color = new Color(0, 0, 0, opacity);
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(4f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScreen");
    }
}
