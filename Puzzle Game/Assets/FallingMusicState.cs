using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingMusicState : MusicState
{

    public MusicState overworldState;
 
    // Start is called before the first frame update
    void Start()
    {
        
    }




    void Update()
    {
        if (motor.getActiveState() == this)
        {
            StateChangeCheck();
        }

    }

    public override void StartPlaying()
    {
        AudioManager.Instance.PlayMusicFadeIn(clip);
    }
   public override void StateChangeCheck()
    {

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(motor.changeState(overworldState)) ;
        }
    }

}
