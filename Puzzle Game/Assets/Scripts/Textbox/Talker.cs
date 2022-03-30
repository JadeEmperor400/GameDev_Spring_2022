using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talker : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer talkIcon = null;

    [SerializeField]
    private Dialogue_Set dialogue = null;

    public bool nearPlayer {
        get {

            return ( true 
                /*(FindObjectOfType<PlayerMovement>() != null) &&
                ( Mathf.Abs( Vector3.Distance(transform.position, GameObject.FindObjectOfType<PlayerMovement>().transform.position) ) <= 1.01f)*/
                );
        }
    }

    public void Update()
    {
        if (dialogue == null) {
            return;
        }

        //Set Speak Icon to true/false
        if (talkIcon != null)
        {
            talkIcon?.gameObject?.SetActive(nearPlayer);
        }

        //if player presses talk button Talk
        if ( nearPlayer && Input.GetKeyDown(KeyCode.E) && !Textbox.On ) {
            Talk();
        }
        
    }

    public void Talk() {
        dialogue?.sendDialogue();
    }
}
