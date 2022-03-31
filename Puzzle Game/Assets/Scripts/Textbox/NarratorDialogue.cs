using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarratorDialogue : DisplayDialogue
{
    
    public Dialogue_Set initialDialogue;

    //commented out for readability
    /*
    public Dialogue_Set drifterLossDialogue;
    public Dialogue_Set drifterWinDialogue;

    public Dialogue_Set octoLossDialogue;
    public Dialogue_Set octoWinDialogue;

    public Dialogue_Set wyrmLossDialogue;
    public Dialogue_Set wyrmWinDialogue;

    public Dialogue_Set anglerLossDialogue;
    public Dialogue_Set anglerWinDialogue;
    */

    public bool isFirstDisplay = false;

    public override void displayFirstDialogue(Dialogue_Set initialDialogueSet)
    {
        initialDialogueSet?.sendDialogue();
        isFirstDisplay = true;
    }
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.CompareTag("Player"))
        if (!isFirstDisplay)
        {
            displayFirstDialogue(initialDialogue);
        }
        //code to freeze the game would go here

    }

   /* public void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            displayFirstDialogue(initialDialogue);
        }

        //something that prevents the player from talking to the narrator again once the dialogue is exhausted
    }*/

    public void OnTriggerExit2D(Collider2D collision)
    {
       
        //ideally something that would stop the narrator hitbox from triggering again
    }

    void Start()
    {

    }

}
