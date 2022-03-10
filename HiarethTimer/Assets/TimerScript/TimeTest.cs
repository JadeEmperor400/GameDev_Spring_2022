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
            Debug.Log("Wassuuup");
            timerSlider.doubleTime();
        }
        if(Input.GetKeyDown("tab")){
            Debug.Log("You pressed tab");
            timerSlider.normalTime();
        }
        if(Input.GetKeyDown("down")){
            Debug.Log("You pressed down");
            Debug.Log("You are removing 2 seconds");
            timerSlider.removeTime();
        }
        if(Input.GetKeyDown("up")){
            Debug.Log("You pressed up");
            Debug.Log("You are adding 2 seconds");
            timerSlider.addTime();
        }
    }
}
