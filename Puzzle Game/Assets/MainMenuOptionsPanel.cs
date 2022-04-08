using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenuOptionsPanel : MonoBehaviour
{


    public GameObject optionsPanel;
    private bool clickedOn = false;
    [SerializeField]
    private Slider sfxSlider;

    [SerializeField]
    private AudioMixer audioMixer;

    public Button buttonToSelectAfterClose;
    public Button buttonToBeSelectedAtFirst;

    // Start is called before the first frame update
    void Start()
    {
        OptionsOff();

        //sfx slider onvaluechange in editor doesnt work, so im scripting it in
        sfxSlider.onValueChanged.AddListener((v) =>
        {
            audioMixer.SetFloat("SFXVol", Mathf.Log10(v) * 20);
        });
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("joystick button 1"))
        {
            OptionsOff();
            DestinationButton(buttonToSelectAfterClose);
        }
        
    }

    public void SetMasterVolume(float masterVolume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(masterVolume) * 20);
    }

    public void SetSFXVolume(float sfxVol) //not being used
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
        FirstButton(buttonToBeSelectedAtFirst);
    }


    public void OptionsOff()
    {
        optionsPanel.SetActive(false);
    }

    public void DestinationButton(Button destinationButton)
    {
        destinationButton.Select();
    }

    public void FirstButton(Button firstButton)
    {
        firstButton.Select();
    }
}
