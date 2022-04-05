using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    //reference to the battle system 

    //reference to all the enemies 




    //Singleton
    private static GameManagerScript instance;
    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad(this.gameObject);

    }


    public void StartBattle(EnemyStats enemyStats)
    {
        //start battle with the enemies stats


    }
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToEndCredits() 
    {

    }
}
