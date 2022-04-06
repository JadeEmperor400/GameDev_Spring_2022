using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MainMenuOptionsPanel : MonoBehaviour
{


    public GameObject optionsPanel;
    private bool clickedOn = false;

    [SerializeField]
    private AudioMixer audioMixer;


    // Start is called before the first frame update
    void Start()
    {
        OptionsOff();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SetMasterVolume(float masterVolume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(masterVolume) * 20);
    }

    public void SetSFXVolume(float sfxVol)
    {
       
        audioMixer.SetFloat("SFXVol", Mathf.Log10(sfxVol) * 20);
    }

    public void SetMusicVolume(float musicVol)
    {
        audioMixer.SetFloat("MusicVol", Mathf.Log10(musicVol) * 20);
    }

    public void OptionsSwitch()
    {
        clickedOn = !clickedOn;

        optionsPanel.SetActive(clickedOn);
    }
    public void OptionsOn()
    {
        optionsPanel.SetActive(true);
    }


    public void OptionsOff()
    {
        optionsPanel.SetActive(false);
    }
}
