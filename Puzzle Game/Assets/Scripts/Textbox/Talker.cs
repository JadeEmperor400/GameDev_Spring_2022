using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talker : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer talkIcon = null;

    [SerializeField]
    private Dialogue_Set dialogue = null;

   

    [SerializeField]
    private float radiusRange = 2f; 


    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collided with box collider");
        }
    }
    
    void OnCollision2D()
    {

    }
    
    public bool nearPlayer {
        get {

            return ( false/*
                (FindObjectOfType<PlayerMovement>() != null) &&
                ( Mathf.Abs( Vector3.Distance(transform.position, GameObject.FindObjectOfType<PlayerMovement>().transform.position) ) <= radiusRange)*/
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
        if ( nearPlayer && Input.GetButtonDown("Jump") && !Textbox.On ) {
            Talk();
        }
        
    }

    public void Talk() {
        dialogue?.sendDialogue();
    }

   
}
