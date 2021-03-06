using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerSlider : MonoBehaviour
{
    public Slider timerSlider;
    public Text timeDisplay;
    public float startTime;
    public float gameTime;
    float timeScaleValue;
    private bool isReset = false;
    public bool isPause = false;

    // Start is called before the first frame update
    void Start()
    {
        //TimerStart(startTime, 0);
    }

    public void TimerStart(float baseTime, float reduction) {
        startTime = baseTime;
        gameTime = startTime;
        timerSlider.maxValue = startTime;
        timerSlider.value = gameTime;
        removeTime(reduction);
        normalTime();
        isPause = false;
        isReset = false;
    }
    // Update is called once per frame
    void Update(){
        if (isPause) {
            return;
        }
        if (FindObjectOfType<GridManager>().Falling) {
            return;
        }

        timeUpdate();  
            if(gameTime <= 0){
                gameTime = startTime;
            isReset = true;
            isPause = true;
                sliderUpdate();
            }
            else
        {
            sliderUpdate();
            isReset = false;
        }
                 
    }

     void sliderUpdate(){
        int minutes = Mathf.FloorToInt(gameTime / 60);
        int seconds = Mathf.FloorToInt(gameTime - minutes * 60f);
        string textTime = string.Format("{0:0}:{1:00}", minutes, seconds);  
        timeDisplay.text = textTime;
        timerSlider.value = gameTime;
    }

    void timeUpdate(){
        gameTime -= Time.deltaTime;
    }


    public void pauseTime(){
        timeScaleValue = Time.timeScale;
        Time.timeScale = 0.1f;
        isPause = true;
        sliderUpdate();             
     }

     public void resumeTime(){
        isPause = false;

        if(timeScaleValue == 2)
            doubleTime();
        else
            normalTime();        
     }  

    public void doubleTime(){
        Time.timeScale = 2;
        sliderUpdate();             
     } 

    public void normalTime(){
        Time.timeScale = 1;
        sliderUpdate();             
     }  

    public void removeTime(){
        gameTime = gameTime - 2;
        sliderUpdate();             
     }

    public void removeTime(float reduction)
    {
        gameTime = gameTime - reduction;
        sliderUpdate();
    }

    public void addTime(){
        gameTime = gameTime + 2;
        sliderUpdate();             
     }    


    //GETTERS AND SETTERS 
    public float getGameTime()
    {
        return gameTime;
    }

    public bool getIsReset()
    {
        return isReset;
    }
    public void SetStartTime(float newStartTime)
    {
        startTime = newStartTime;
    }

    public float GetStartTime()
    {
        return startTime;
    }
   
}
