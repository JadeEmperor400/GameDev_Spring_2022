using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTest : MonoBehaviour
{
    public TimerSlider timerSlider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space")){
            //Debug.Log("Time is counting down twice as fast");
            timerSlider.doubleTime();
        }
        if(Input.GetKeyDown("tab")){
            //Debug.Log("Time is counting down regularly");
            timerSlider.normalTime();
        }
        if(Input.GetKeyDown("down")){
            //Debug.Log("You pressed down");
            //Debug.Log("You are removing 2 seconds");
            timerSlider.removeTime();
        }
        if(Input.GetKeyDown("up")){
            //Debug.Log("You pressed up");
            //Debug.Log("You are adding 2 seconds");
            timerSlider.addTime();
        }
        if(Input.GetKeyDown("left")){
            //Debug.Log("You pressed left");
            //Debug.Log("time FREEZE");
            timerSlider.pauseTime();
        }
        if(Input.GetKeyDown("right")){
            //Debug.Log("You pressed right");
            //Debug.Log("time UNFREEZE");
            timerSlider.resumeTime();
        }
    }
}
