using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum State{
        PlayerPhase, EnemyPhase, GameOver, Victory
    };

public class BattleManager : MonoBehaviour
{
    public GridManager gridManager;
    public GridComboManager comboManager;
    public TimerSlider timer;
    public PlayerStats player;
    public List<EnemyStats> enemy;

    public PlayerAction redPlayerAction;
    public PlayerAction bluePlayerAction;
    public PlayerAction greenPlayerAction;




    public State state{
        get; 
        private set;
    }=State.PlayerPhase;

    
    void Start()
    {
        playerTurn();
    }


    void Update()
    {
        
    }

    private void playerTurn(){
        // Opens puzzle grid (calls puzzleOpen method)
        puzzleOpen();

        // Waits for player to finish their puzzle (calls playerCalc method)


        // Take players attack type to choose attack, calculate support effects and dmg (calls playerCalc method)
        
    }

    public void puzzleOpen(){
        //Method that opens puzzle grid  
        gridManager.gameObject.SetActive(true);//turn on gridmanager
        timer.gameObject.SetActive(true);//turn on slider



        //Apply Time Reduction to timer
        
    }

    public void playerCalc(){
        // Play Attack Animation

        // Close Puzzle

        // Calculate Dmg, Heals, and Debuffs
            // Get Combo Info
            // First connection color determines attack
            //Subsequent connections stack support effects
        CalculatePlayerAttack(comboManager.currentComboQueue());


        //apply attack to enemy


        // Check Transition to EnemyPhase or Victory
            // Check all enemies
                // If 1 or more enimies have more than 0HP
                    // Move to enemy phase
                // else
                    // Move to victory screen and end the battle
        for (int i = 0; i < enemy.Count; i++)
        {
           if (enemy[i].HP > 0)
            {
                //moving to enemy enemy phase
                //enemyPhase();
                //state = State.EnemyPhase;
            }
        }

        //no enemies have health greater than 0, moving to victory screen
        //state = State.Victory;
    }

    public void enemyPhase(){
        // Randomize Enemy Turn order
        List<EnemyStats> randomizedEnemies;
        randomizedEnemies = RandomizeEnemyTurnOrder(enemy);


        // For each enemy
        // Execute enemy behavior script
        // Play Action Animation
        // Calculate Dmg, Healing, and Debuffs
        // Check if the player has less than 0HP
        // If so, move to Game Over
        // If not, return to playerTurn   
        foreach (var i in randomizedEnemies)
        {

        }
    }


    private void CalculatePlayerAttack(Queue<Connection> currentCombo)
    {
        int supportDMG = 0; 

        if(currentCombo == null) //if player did no connections, then leave this method 
            return;

        var firstConnection = currentCombo.Dequeue();
        ColorEnum firstConnectionColorType  = firstConnection.getColorType();
        int baseDamage = DetermineBaseDamage(firstConnectionColorType); 

       


        for(int i =1; i < currentCombo.Count; i++ )
        {
            //subsequent attacks
            var combo = currentCombo.Dequeue();
            var colorType = combo.getColorType();
            supportDMG += DetermineSupportDamage(colorType); 
        }

       
        DealDamage(baseDamage, supportDMG);
    }

    private void DealDamage(int baseDMG, int supportDMG)
    {
        
        enemy[0].TakeDamage(baseDMG); 
        enemy[0].TakeDamage(supportDMG);
    }
    private int DetermineSupportDamage(ColorEnum colorType)
    {

        switch (colorType)
        {
            case ColorEnum.RED:

               
                return 20;
               
            case ColorEnum.GREEN:
                return 0; 
              
            case ColorEnum.BLUE:

                return 0;
            default:
                break;
        }

        return 0;
    }

    private int DetermineBaseDamage(ColorEnum colorType)
    {
        switch(colorType)
        {
            case ColorEnum.RED:
               
               int baseAtk = redPlayerAction.power;
                return baseAtk;
               
            case ColorEnum.GREEN:
               
                break;
            case ColorEnum.BLUE:
              
                break;
                default:
                break;
        }


        return -500; //sanity check
    }


    private void ApplyEnemyAttack(int damage, int stagger)
    {
        enemy[0].TakeDamage(damage);
    }

    public List<EnemyStats> RandomizeEnemyTurnOrder(List<EnemyStats> OrderedEnemyStats)
    {


        return null;
    }

}
