using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipEndCreds : MonoBehaviour
{
    public string StartingScreen;

    public void LoadStart()
    {
        SceneManager.LoadScene(StartingScreen);
    }
}