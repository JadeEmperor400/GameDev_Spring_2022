using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerTest : MonoBehaviour
{
   
    public AudioClip music1;
    public AudioClip music2;
    public AudioClip music3;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("pressed Z");
            AudioManager.Instance.PlayMusicFadeIn(music1);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("pressed X");
            AudioManager.Instance.StopMusicFadeOut();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("pressed X");
            AudioManager.Instance.PlayMusicFadeIn(music3);
        }


        if (Input.GetKeyDown(KeyCode.Alpha2))
            AudioManager.Instance.PlayMusic(music1);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            AudioManager.Instance.PlayMusic(music2);

        if (Input.GetKeyDown(KeyCode.Alpha4))
            AudioManager.Instance.PlayMusicWithFade(music1);

        if (Input.GetKeyDown(KeyCode.Alpha5))
            AudioManager.Instance.PlayMusicWithFade(music2);

        if (Input.GetKeyDown(KeyCode.Alpha6))
            AudioManager.Instance.PlayMusicWithCrossFade(music1, 3.0f);

        if (Input.GetKeyDown(KeyCode.Alpha7))
            AudioManager.Instance.PlayMusicWithCrossFade(music2, 3.0f);
    }
}