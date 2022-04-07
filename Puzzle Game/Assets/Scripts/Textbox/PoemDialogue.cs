using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoemDialogue : DisplayDialogue
{

    /*
    [SerializeField]
    private SpriteRenderer talkIcon = null;
    */
    public PlayerMovement playerMovement;


    public float radius = 1.5f;

    public Dialogue_Set poemDialogue = null;
    //no bool since player should be able to reread

    public override void displayFirstDialogue(Dialogue_Set poemDialogueSet)
    {
        playerMovement.FreezePlayer();
        poemDialogueSet?.sendDialogue();
        
    }

    public bool nearPlayer
    {
        get
        {

            return (
                (FindObjectOfType<PlayerMovement>() != null) &&
                ( Mathf.Abs( Vector3.Distance(transform.position, GameObject.FindObjectOfType<PlayerMovement>().transform.position) ) <= radius)
                );
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(playerMovement == null)
            playerMovement = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (nearPlayer && Input.GetKeyDown(KeyCode.E) && !Textbox.On)
        {
            displayFirstDialogue(poemDialogue);
        }

        /*
        if (talkIcon != null)
        {
            talkIcon?.gameObject?.SetActive(nearPlayer);
        }
        */
    }
}
