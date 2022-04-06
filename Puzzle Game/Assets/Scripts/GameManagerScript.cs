using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    //reference to the battle system 

    //reference to all the enemies 

    public BattleManager battleManager;
   

    public List<EnemyStats> coralWyrm;

    //Singleton
    private static GameManagerScript instance;
    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad(this.gameObject);

        if(battleManager == null)
            battleManager = FindObjectOfType<BattleManager>();  

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            StartBattle();
        }           
    }
    public void StartBattle()
    {       
        battleManager.BeginBattle(coralWyrm);
    }

    public void StartBossBattle()
    {
        
    }
   
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToEndCredits() 
    {

    }
}
