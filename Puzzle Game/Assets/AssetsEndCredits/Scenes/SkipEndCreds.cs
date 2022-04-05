using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipEndCreds : MonoBehaviour
{
    public string StartingScreen;
    public GameObject skipbutton;
    public void LoadStart()
    {
        skipbutton.SetActive(false);
        SceneManager.LoadScene(StartingScreen);
    }
}