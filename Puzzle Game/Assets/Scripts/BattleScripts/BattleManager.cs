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


    public State state{
        get; 
        private set;
    }=State.PlayerPhase;


    

    
    // Start is called before the first frame update
    void Start()
    {
        playerTurn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void playerTurn(){
        // Opens puzzle grid (calls puzzleOpen method)
        // Waits for player to finish their puzzle (calls playerCalc method)
        // Take players attack type to choose attack, calculate support effects and dmg (calls playerCalc method)
        
    }

    public void puzzleOpen(){
        //Apply Time Reduction to timer
        //Method that opens puzzle grid  
    }

    public void playerCalc(){
        // Play Attack Animation
        // Close Puzzle
        // Calculate Dmg, Heals, and Debuffs
            // Get Combo Info
                // First connection color determines attack
                    //Subsequent connections stack support effects
        // Check Transition to EnemyPhase or Victory
            // Check all enemies
                // If 1 or more enimies have more than 0HP
                    // Move to enemy phase
                // else
                    // Move to victory screen and end the battle
    }

    public void enemyPhase(){
        // Randomize Enemy Turn order
        // For each enemy
            // Execute enemy behavior script
                // Play Action Animation
                // Calculate Dmg, Healing, and Debuffs
                // Check if the player has less than 0HP
                    // If so, move to Game Over
                    // If not, return to playerTurn   
    }
}
