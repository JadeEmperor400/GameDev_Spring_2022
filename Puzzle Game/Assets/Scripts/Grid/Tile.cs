using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    


    [SerializeField]
    private SpriteRenderer _renderer;

    [SerializeField]
    private GameObject lineObjectPrefab;




    public void Init(bool isOffset) //for the init, perhaps change it to ethier, red green blue or empty 
    {
        if (isOffset)
            _renderer.color = Color.blue;
        else
            _renderer.color = Color.yellow;

        var spawnedLineObject = Instantiate(lineObjectPrefab, transform.position, Quaternion.identity);
        spawnedLineObject.name = name + " lineObject";

        spawnedLineObject.transform.parent = transform;

    }

    void OnMouseEnter()
    {

    }

    //initialize a among us style line rendererer in each tile. then make it only valid for the tiles with a "color" in it 
    //If I click on that line I can grab it and infinitely drag it to another "grid" 
    //look at the snap to object code, maybe you can add that as well, the line renderer can only "snap" to its same color "line and line catcher object?" 



    //future, what happens if a connection is valid? 


}
