using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    //reference to the battle system 

    //reference to all the enemies 


    public GameObject helpPanel;
    private bool switcher = false;
    public MusicMotor musicMotor;
    public MusicState battleMusicState;
    public MusicState overworldState;





    //Singleton
    private static GameManagerScript instance;
    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad(this.gameObject);



        helpPanel.SetActive(false);


        if (battleMusicState == null)
            battleMusicState = FindObjectOfType<BattleMusicState>();
        if (musicMotor == null)
            musicMotor = FindObjectOfType<MusicMotor>();
        if (overworldState == null)
            overworldState = FindObjectOfType<OverworldMusicState>();

    }



    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            switcher = !switcher;
            helpPanel.SetActive(switcher);
        }
    }

    public void StartBattle()
    {
        StartCoroutine(musicMotor.changeState(battleMusicState));
        //start battle with the enemies stats


    }
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToEndCredits() 
    {

    }
}
