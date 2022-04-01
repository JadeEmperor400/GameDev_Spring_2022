using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreBossTensionMusicState : MusicState
{
    public MusicState overworldMusicState;
    //public MusicState bossBattleMusicState;
    private bool inArea = false;


    private PlayerMovement playerMovement;
    //a unique dummy boss script here, we will calculate the distance from player to the boss 
    public float radius = 5.5f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement = FindObjectOfType<PlayerMovement>(); 
        if(playerMovement == null)
        {
            return;
        }


        if (motor.getActiveState() == this)
        {
            StateChangeCheck();
        }
    }

    public override void StartPlaying()
    {
        // base.StartPlaying();
        AudioManager.Instance.PlayMusicFadeIn(clip);
    }

    public override void StopPlaying()
    {
        AudioManager.Instance.StopMusicFadeOut();
    }
    public override void StateChangeCheck()
    {
       if((Mathf.Abs(Vector3.Distance(transform.position, GameObject.FindObjectOfType<PlayerMovement>().transform.position)) <= radius))
            StartCoroutine(motor.changeState(overworldMusicState));
        

        if(Input.GetKeyDown(KeyCode.J))
        {
            //start coroutine to boss battle music 
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        inArea = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inArea = false;
    }

    public bool getInArea()
    { 
        return inArea; 
    }
}
