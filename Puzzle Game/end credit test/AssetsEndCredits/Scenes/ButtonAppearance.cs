using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAppearance : MonoBehaviour
{

    public GameObject SkipButton;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(HideAndShow(8.0f));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator HideAndShow(float delay)
    {
        SkipButton.SetActive(false);
        yield return new WaitForSeconds(delay);
        SkipButton.SetActive(true);
    }
}