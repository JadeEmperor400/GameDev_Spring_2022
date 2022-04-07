using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridAudio : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    public AudioClip RedChargeUpClip;
    public AudioClip BlueChargeUpClip;
    public AudioClip GreenChargeUpClip;

    private List<GameObject> tiles = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlayChargeUpSound(List<GameObject> tiles)
    {
       
        //assumption that grid manager, did all the checks if its a valid connection 
        ColorEnum colorType =  tiles[0].gameObject.GetComponent<Tile>().GetTileColorIdentity();
       
        switch(colorType)
        {
            case ColorEnum.RED:
                audioSource.clip = RedChargeUpClip;
                PlaySound();
                break;
            case ColorEnum.BLUE:
                audioSource.clip= BlueChargeUpClip;
                PlaySound();
                break;
            case ColorEnum.GREEN:
               
                audioSource.clip = GreenChargeUpClip;
                PlaySound();
                break;
            default:
               
                break;

        }


    }

    public void PlaySound() //playing sound method with some small randomization features 
    {
        
        audioSource.pitch = 1f + Random.Range(-0.2f, 0.2f);
        audioSource.PlayOneShot(audioSource.clip);
    }

}
