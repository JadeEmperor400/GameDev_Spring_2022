using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameManager : MonoBehaviour
{

    //simple test class to combine the timer, combo and puzzle system 
    [SerializeField]
    private GridManager gridManager;
    [SerializeField]
    private TimerSlider timerSlider;
    [SerializeField]
    private GridComboManager gridComboManager;

    [SerializeField]
    private float startTime;

    private void Start()
    {
        timerSlider.SetStartTime(startTime);
        startTime = timerSlider.GetStartTime();
        timerSlider.normalTime();
    }
    private void Update()
    {
        if (gridManager.Falling)
        {
            timerSlider.pauseTime();
            return;
        }
        else {
            timerSlider.resumeTime();
        }

        if (gridComboManager.currentComboQueue().Count == 0)
        {
          
            //half speed
            timerSlider.normalTime();
        }
        else
        {
           
            //normal speed 
            timerSlider.doubleTime();
        }

        if (timerSlider.getIsReset() == true)
        {
            gridManager.RegenerateGrid();
        }
    }


}
