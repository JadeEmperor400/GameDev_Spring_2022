using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarratorDialogue : DisplayDialogue
{
    
    public Dialogue_Set initialDialogue;

    public bool isFirstDisplay = false;

    public override void displayFirstDialogue(Dialogue_Set initialDialogueSet)
    {
        initialDialogueSet?.sendDialogue();
        isFirstDisplay = true;
    }
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("You have entered the collider. Dialogue should change automatically");
        if (!isFirstDisplay)
        {
            displayFirstDialogue(initialDialogue);
        }
        //code to freeze the game would go here

    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            displayFirstDialogue(initialDialogue);
        }

        //something that prevents the player from talking to the narrator again once the dialogue is exhausted
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("You have exited the collider. E will no longer do anything");
        //ideally something that would stop the narrator hitbox from triggering again
    }

}
