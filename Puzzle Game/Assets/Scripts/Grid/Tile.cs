using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{


    //TODO potentially seperate things about line object into a seperate script 

   
    private ColorEnum colorIdentity;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private GameObject lineObjectPrefab;


    private GridManager gridManager; 

    private GameObject spawnedLineObject;


    void Update()
    {
       
    }
    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
    }

    public void Init() 
    {
        AssignColorIdentity();
    }

    private void AssignColorIdentity()
    {
        int randNum = Random.Range(0, 5);

        switch (randNum)
        {
            case 0:
                colorIdentity = ColorEnum.RED;
                spriteRenderer.color = Color.red;
                break;

            case 1:
                colorIdentity = ColorEnum.BLUE;
                spriteRenderer.color = Color.blue;
                break;

            case 2:
                colorIdentity = ColorEnum.GREEN;
                spriteRenderer.color = Color.green;
                break;

            case 3:
                colorIdentity = ColorEnum.NONE;
                spriteRenderer.color = Color.black;
                break;

            case 4:
                colorIdentity = ColorEnum.NONE;
                spriteRenderer.color = Color.black;
                break;

            default:
                Debug.Log("Default case reached in Tile class");
                colorIdentity = ColorEnum.NONE;
                spriteRenderer.color = Color.black;
                break;
        }
    }

   

    private void CreateLineObject()
    {
            spawnedLineObject = Instantiate(lineObjectPrefab, transform.position, Quaternion.identity);
            spawnedLineObject.name = name + " lineObject";
            spawnedLineObject.transform.parent = transform;
    }
    
    public void DestroyLineObject()
    {
        if (spawnedLineObject != null)
        {
            Destroy(spawnedLineObject);
        }
    }

    //method for the NONE tiles 
    private void OnTriggerEnter2D(Collider2D col)
    {
       if(colorIdentity == ColorEnum.NONE)
        {
            if (col.gameObject.CompareTag("LineHead"))
            {

                SnapToPosition(col.gameObject);
                gridManager.AddConnectedTiles(this.gameObject);
                if (spawnedLineObject == null)
                {
                    CreateLineObject();
                }
                

            }
                
        }
       
       if(colorIdentity != ColorEnum.NONE)
        {
            if(col.gameObject.CompareTag("LineHead"))
            {
                //send to gridmanager to determine if its a connection
                //if it is a valid connection(bool method?), snap to position. And remove all line head in that list and reset the list ?
                //remember check for if it hits itself 
               

                var firstTile = gridManager.getFirstConnectedTile();
                if(GameObject.ReferenceEquals(firstTile, this.gameObject))
                {
                    return; //this is when the lineobject hits itself
                }


                if(firstTile.GetComponent<Tile>().GetTileColorIdentity() == GetTileColorIdentity())
                {
                    Debug.Log("Connection made"); //why does the tile class have info on when a connection is made 
                    SnapToPosition(col.gameObject);
                    
                    gridManager.AddConnectedTiles(this.gameObject);
                }
            }
        }


            

       
    }

   

    private void SnapToPosition(GameObject lineObject)
    {
        var lr = lineObject.gameObject.transform.parent.GetComponentInChildren<LineRenderer>();
        var Line = lineObject.gameObject.GetComponent<Line>();
        Line.DisableDrag();
        lineObject.gameObject.transform.position = transform.position;
        lr.SetPosition(2, new Vector3(lineObject.gameObject.transform.localPosition.x, lineObject.gameObject.transform.localPosition.y, 0f));
    }


    private void OnMouseDown()
    {
        if ((spawnedLineObject == null) && colorIdentity != ColorEnum.NONE)
        {
           
            gridManager.AddConnectedTiles(this.gameObject);
            CreateLineObject();
        }          
    }
  

    public ColorEnum GetTileColorIdentity()
    {
        return colorIdentity;
    }

}
