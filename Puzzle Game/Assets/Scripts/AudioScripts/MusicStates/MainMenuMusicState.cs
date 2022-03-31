using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuMusicState : MusicState
{
    Scene scene;
   public MusicState overworldState;





    void Start()
    {
         scene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if(motor.getActiveState() == this)
        StateChangeCheck();
    }

    public override void StartPlaying()
    {
        AudioManager.Instance.PlayMusicFadeIn(clip);
    }

    public override void StateChangeCheck()
    {
       
      
        
        //if the current scene is not main menu, go to overworld scene 
        if(scene != SceneManager.GetActiveScene())
        {
           
          StartCoroutine(motor.changeState(overworldState));
           
        }
    }


    public override void StopPlaying()
    {
        AudioManager.Instance.StopMusicFadeOut();
    }
}
