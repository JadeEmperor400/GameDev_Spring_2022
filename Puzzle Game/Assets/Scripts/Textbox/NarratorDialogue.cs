using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarratorDialogue : DisplayDialogue
{
    //currently unused
    //public Dialogue_Set additionalDialogueSet;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("You have entered the collider. Dialogue should change automatically");
        displayFirstDialogue();
        if (Input.GetKeyDown(KeyCode.E))
        {
            displayFirstDialogue();
        }
        //code to freeze the game would go here
        //if additionalDialogue != null then say additionalDialogue
        //would the narrator have additional dialogue or would they disappear after speaking once?
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("You have exited the collider. E will no longer do anything");
        //ideally something that would invalidate prevent the player from triggering the collider more than once
        //maybe deleting the collider?
    }

    //i left this for future reference but everything below is not needed at all
    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool nearPlayer
    {
        get
        {
            return false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (additionalDialogueSet == null)
        {
            return;
        }
        

        
        //Set Speak Icon to true/false
        if (talkIcon != null)
        {
            talkIcon?.gameObject?.SetActive(nearPlayer);
        }
        

        //if player presses talk button Talk
        if (nearPlayer && !Textbox.On)
        {
            displayFirstDialogue();

            if (Input.GetKeyDown(KeyCode.E))
            {
                displayFirstDialogue();
            }
        }
    }
    */


}
