using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDialogue : DisplayDialogue
{
    public PlayerMovement playerMovement;
  
    
    public float radius = 1.5f;

    public Dialogue_Set introEnemyDialogue = null;
    public Dialogue_Set fightEnemyDialogue = null;
    public Dialogue_Set winEnemyDialogue = null;
    public Dialogue_Set lossEnemyDialogue = null;

    public override void displayFirstDialogue(Dialogue_Set enemyDialogueSet)
    {
        playerMovement.FreezePlayer();
        enemyDialogueSet?.sendDialogue();
        fightEnemyDialogue?.sendDialogue();
    }

    public void displayWinEnemyDialogue()
    {
        winEnemyDialogue?.sendDialogue();
    }

    public void displayLossEnemyDialogue()
    {
        lossEnemyDialogue?.sendDialogue();
    }

    public bool nearPlayer
    {
        get
        {
            return (
                (FindObjectOfType<PlayerMovement>() != null) &&
                (Mathf.Abs(Vector3.Distance(transform.position, GameObject.FindObjectOfType<PlayerMovement>().transform.position)) <= radius)
                );
        }
    }

  

    // Update is called once per frame
    void Update()
    {
        if (nearPlayer && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse0)) && !Textbox.On)
        {
            displayFirstDialogue(introEnemyDialogue);
        }
    }
}
