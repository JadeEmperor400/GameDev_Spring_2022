using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerSlider : MonoBehaviour
{
    public Slider timerSlider;
    public Text timeDisplay;
    public float gameTime;

    // Start is called before the first frame update
    void Start()
    {
        gameTime = 10;
        timerSlider.maxValue = gameTime;
        timerSlider.value = gameTime;
    }

    // Update is called once per frame
    void Update()
    {
        float time = gameTime - Time.time;
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time - minutes * 60f);
        string textTime = string.Format("{0:0}:{1:00}", minutes, seconds);    

        if(time <=0){
            Start();
            timeDisplay.text = textTime;
            timerSlider.value = time;
        }
        if(time >= 0){
            if(Input.GetKeyDown("space")){
            time = gameTime - Time.time*2;
            timeDisplay.text = textTime;
            timerSlider.value = time;           
        }
            else{
            timeDisplay.text = textTime;
            timerSlider.value = time;
            }
           
    }


    }
}
