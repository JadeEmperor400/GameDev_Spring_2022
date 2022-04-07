using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyType { normalEnemy, BossEnemy}
public class EnemyDialogue : DisplayDialogue
{
    

    public PlayerMovement playerMovement;
    public EnemyType enemyType = EnemyType.normalEnemy; 
    public GameManagerScript gameManagerScript;
    public float radius = 1.5f;

    public Dialogue_Set introEnemyDialogue = null;
    public Dialogue_Set fightEnemyDialogue = null;
    public Dialogue_Set winEnemyDialogue = null;
    public Dialogue_Set lossEnemyDialogue = null;

    public List<EnemyStats> battle;

    public static int EnemyCount = 0;
    [SerializeField]
    private int myID;

    public override void displayFirstDialogue(Dialogue_Set enemyDialogueSet)
    {
        playerMovement.FreezePlayer();
        enemyDialogueSet?.sendDialogue();
        fightEnemyDialogue?.sendDialogue();
        if (battle != null && battle.Count > 0) {
            GameManagerScript.instance.SetNextbattle(battle);
            EventSystem.eventController.OnBattleEnd += MyEvent;
            EventSystem.eventController.killID = myID;
        }
        //gameManagerScript.StartBattle();
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


    private void Start()
    {
        myID = EnemyCount;
        EnemyCount++;

        if(playerMovement == null)
            playerMovement = FindObjectOfType<PlayerMovement>();    
    }


    // Update is called once per frame
    void Update()
    {
        if (nearPlayer && !Textbox.On)
        {
            displayFirstDialogue(introEnemyDialogue);
        }
    }

    private void OnMouseDown()
    {
       /* if(nearPlayer && !Textbox.On)
        {
            displayFirstDialogue(introEnemyDialogue);
        }*/
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            displayFirstDialogue(introEnemyDialogue);
        }
    }

    public EnemyType GetEnemyType()
    {
        return enemyType;
    }

    public void MyEvent(int id = -1) {
        if (id == myID) {
            switch (enemyType) {
                case EnemyType.normalEnemy:
                    Destroy(gameObject);
                    Debug.Log("Dissappear");
                    break;
                case EnemyType.BossEnemy:
                    Destroy(gameObject);
                    Debug.Log("Go To Credits");
                    break;
            }
        }
        return;
    }
}
