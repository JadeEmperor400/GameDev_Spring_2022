using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToTitle : MonoBehaviour
{
    public string StartingScreen;
    public GameObject EndCreds;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(HideAndShow(19.0f));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator HideAndShow(float delay)
    {
        EndCreds.SetActive(true);
        yield return new WaitForSeconds(delay);
        LoadStart();
    }
    

    public void LoadStart()
    {
        SceneManager.LoadScene(StartingScreen);
    }
}