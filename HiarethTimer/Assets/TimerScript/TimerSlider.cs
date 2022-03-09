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
        gameTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(gameTime / 60);
        int seconds = Mathf.FloorToInt(gameTime - minutes * 60f);
        string textTime = string.Format("{0:0}:{1:00}", minutes, seconds);    

        if(gameTime <=0){
            Start();
            timeDisplay.text = textTime;
            timerSlider.value = gameTime;
        }
        if(gameTime >= 0){
            if(Input.GetKeyDown("space")){
            gameTime = gameTime - Time.deltaTime*2;
            timeDisplay.text = textTime;
            timerSlider.value = gameTime;           
        }
            else{
            timeDisplay.text = textTime;
            timerSlider.value = gameTime;
            }
           
    }


    }
}
