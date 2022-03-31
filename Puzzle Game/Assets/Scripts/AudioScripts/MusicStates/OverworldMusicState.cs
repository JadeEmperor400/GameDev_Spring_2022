using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldMusicState : MusicState
{
    public MusicState battleMusicState;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(motor.getActiveState() == this)
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
        if(Input.GetKeyDown(KeyCode.J))
        {
            StartCoroutine(motor.changeState(battleMusicState));
        }



    }


}
