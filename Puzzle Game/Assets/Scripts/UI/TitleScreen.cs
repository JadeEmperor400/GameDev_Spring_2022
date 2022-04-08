using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TitleScreen : MonoBehaviour
{
    public AudioClip mainmenuMusic;

    public Button firstButton;


    // Start is called before the first frame update
    private void Start()
    {
        AudioManager.Instance.PlayMusicFadeIn(mainmenuMusic);
        DestinationButton(firstButton);
    }
    public void PlayDemo()
    {
        //Scenes list.
        //0 is Title Screen. 1 is Overworld Demo
        //If you need which scene we are in: SceneManager.GetActiveScene().buildIndex
        SceneManager.LoadScene(1);
        AudioManager.Instance.StopMusicFadeOut();


    }

    public void PlayCredits()
    {
        AudioManager.Instance.StopMusicFadeOut();
        SceneManager.LoadScene(2);
    }


    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void DestinationButton(Button button)
    {
        button.Select();
    }
}
