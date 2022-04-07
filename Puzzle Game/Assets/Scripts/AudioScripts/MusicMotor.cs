using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMotor : MonoBehaviour
{
    public MusicState activeState;
    public MusicState intialState;

    // Start is called before the first frame update
    void Start()
    {
        activeState = intialState;
        activeState.StartPlaying();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(activeState);
    }

    public IEnumerator changeState(MusicState state)
    {
        //Debug.Log(state.ToString());
        activeState.StopPlaying();
        //2-3 sec buffer wait time here 
        yield return new WaitForSeconds(2);
        activeState = state;
        activeState.StartPlaying();


    }

    public MusicState getActiveState()
    {
        return activeState;
    }
   
}
