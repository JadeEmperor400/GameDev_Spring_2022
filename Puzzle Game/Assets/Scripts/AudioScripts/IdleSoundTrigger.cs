using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleSoundTrigger : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_AudioSource;

    [SerializeField]
    private List<AudioClip> m_AudioClips;

    [SerializeField]
    private MusicMotor m_Motor;

    [SerializeField]
    private PreBossTensionMusicState preBossTensionMusicState;
    [SerializeField]
    private OverworldMusicState overworldMusicState;

    void Start()
    {
      if(m_Motor == null)
            m_Motor = FindObjectOfType<MusicMotor>().GetComponent<MusicMotor>();

      if(preBossTensionMusicState == null)
            preBossTensionMusicState = FindObjectOfType<PreBossTensionMusicState>().GetComponent<PreBossTensionMusicState>();

        if (overworldMusicState == null)
            overworldMusicState = FindObjectOfType<OverworldMusicState>().GetComponent<OverworldMusicState>();


    }

   
    void Update()
    {


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if(collision.CompareTag("Player"))
        {

           StartCoroutine( m_Motor.changeState(preBossTensionMusicState));



            m_AudioSource.clip = m_AudioClips[0];
            m_AudioSource.Play();
        }
        
        
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            StartCoroutine(m_Motor.changeState(overworldMusicState));



           
        }
    }
}
