using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MusicState : MonoBehaviour
{

    public MusicMotor motor;
    
    public AudioClip clip; 

    public virtual void StateChangeCheck()
    {

    }




    public virtual void StartPlaying()
    {

    }


    public virtual void StopPlaying()
    {

    }
}
