using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public AudioClip mainmenuMusic;
   

    // Start is called before the first frame update
    private void Start()
    {
        AudioManager.Instance.PlayMusicFadeIn(mainmenuMusic);
    }
    public void PlayGame()
    {
        //Scenes list.
        //0 is StartMenu. 1 is Overworld Demo
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        AudioManager.Instance.StopMusicFadeOut();


    }

    public void QuitGame()
    { 
        Debug.Log("Quit");
        Application.Quit();
    }
}
