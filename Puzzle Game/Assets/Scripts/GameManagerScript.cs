using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    //reference to the battle system 

    //reference to all the enemies 

    public BattleManager battleManager;

    public List<EnemyStats> nextBattle = null;
    public List<EnemyStats> coralWyrm;
    public GameObject helpPanel;
    private bool switcher = false;
    public MusicMotor musicMotor;
    public MusicState battleMusicState;
    public MusicState overworldState;

    //Singleton
    public static GameManagerScript instance;
    private void Awake()
    {

        instance = this;

        if(battleManager == null)
            battleManager = FindObjectOfType<BattleManager>();

        helpPanel?.SetActive(false);


        if (battleMusicState == null)
            battleMusicState = FindObjectOfType<BattleMusicState>();
        if (musicMotor == null)
            musicMotor = FindObjectOfType<MusicMotor>();
        if (overworldState == null)
            overworldState = FindObjectOfType<OverworldMusicState>();

    }

    

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            StartBattle();
        }   
         if(Input.GetKeyDown(KeyCode.H))
        {
            switcher = !switcher;
            helpPanel.SetActive(switcher);
        }        
    }
    public void StartBattle()
    {   
        if(nextBattle == null || nextBattle.Count <= 0){

            return;
        }
        battleManager.BeginBattle(nextBattle);
        StartCoroutine(musicMotor.changeState(battleMusicState));
    }

    public void SetNextbattle(List<EnemyStats> enemyStats) {
        if(enemyStats == null || enemyStats.Count <= 0){
            return;
        }

        nextBattle = new List<EnemyStats>();

        for (int i = 0; i < enemyStats.Count; i++) {
            if (enemyStats[i] == null) {
                continue;
            }
            nextBattle.Add(enemyStats[i]);
        }

        if (nextBattle.Count == 0) {
            nextBattle = null;
        }
    }

    public void ClearNextBattle() {
        if (nextBattle == null) { return; }
        nextBattle.Clear();
        nextBattle = null;
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
