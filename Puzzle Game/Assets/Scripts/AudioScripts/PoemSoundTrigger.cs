using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoemSoundTrigger : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
     if(audioSource == null)
            audioSource = GetComponent<AudioSource>();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            audioSource.Play();
    }
}
