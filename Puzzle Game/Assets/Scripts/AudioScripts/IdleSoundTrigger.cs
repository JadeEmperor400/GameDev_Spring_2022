using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleSoundTrigger : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_AudioSource;

    [SerializeField]
    private List<AudioClip> m_AudioClips;

  
    void Start()
    {
      
    }

   
    void Update()
    {


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hello?");
        if(collision.CompareTag("Player"))
        {
            m_AudioSource.clip = m_AudioClips[0];
            m_AudioSource.Play();
        }
        
        
    }
}
