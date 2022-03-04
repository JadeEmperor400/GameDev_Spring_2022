using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private const int X_OFFSET = -2;
    private const int Y_OFFSET = -3;
    private ColorEnum colorIdentity;

    [SerializeField]
    private SpriteRenderer _renderer;

    [SerializeField]
    private GameObject lineObjectPrefab;

    private GridManager gridManager; 

    private GameObject spawnedLineObject;


    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        
       
    }
    public void Init() 
    {
        
        int randNum = Random.Range(0, 5);

        switch(randNum)
        {
                case 0:
                colorIdentity = ColorEnum.RED;
                break; 

                case 1:
                colorIdentity = ColorEnum.BLUE;
                break;

                case 2:
                colorIdentity = ColorEnum.GREEN;
                break; 

                case 3:
                colorIdentity = ColorEnum.NONE;
                break;

                case 4:
                colorIdentity = ColorEnum.NONE;
                break;

                default:
                Debug.Log("Default case reached in Tile class");

                colorIdentity = ColorEnum.NONE;
                break;
    

        }
        setColor();
    }

    private void setColor()
    {
        switch (colorIdentity)
        {
            case ColorEnum.RED:
                _renderer.color = Color.red;
               // createLineObject();
                break;

            case ColorEnum.BLUE:
                _renderer.color = Color.blue;
               // createLineObject();
                break;
            case ColorEnum.GREEN:
                _renderer.color = Color.green;
               // createLineObject();
                break;
            case ColorEnum.NONE:
                _renderer.color = Color.black;
                break;

            default:
                Debug.Log("Default case reached in Tile class, SetColor()");
               
                break;
        }
    }

    private void createLineObject()
    {
      
            spawnedLineObject = Instantiate(lineObjectPrefab, transform.position, Quaternion.identity);
            spawnedLineObject.name = name + " lineObject";
            spawnedLineObject.transform.parent = transform;
        

    }
    
    public void destroyLineObject()
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
              
                var lr = col.gameObject.transform.parent.GetComponentInChildren<LineRenderer>();
          
                var Line = col.gameObject.GetComponent<Line>();
                Line.DisableDrag();
                col.gameObject.transform.position = transform.position;

                gridManager.AddConnectedTiles(this.gameObject);

                lr.SetPosition(2, new Vector3(col.gameObject.transform.localPosition.x, col.gameObject.transform.localPosition.y, 0f));


                if (spawnedLineObject == null)
                {
                    createLineObject();
                }
                

            }
                
        }
       
        

       if(colorIdentity != ColorEnum.NONE)
        {
            if(col.gameObject.CompareTag("LineHead"))
            {
                var firstTile = gridManager.getFirstConnectedTile();
                if(GameObject.ReferenceEquals(firstTile, this.gameObject))
                {
                    return; //this is when the lineobject hits itself
                }


                if(firstTile.GetComponent<Tile>().getTileColorIdentity() == getTileColorIdentity())
                {
                    Debug.Log("Connection made");
                    //REPEATING CODE, THIS SHOULD BE A METHOD 
                    var lr = col.gameObject.transform.parent.GetComponentInChildren<LineRenderer>();

                    var Line = col.gameObject.GetComponent<Line>();
                    Line.DisableDrag();
                    col.gameObject.transform.position = transform.position;

                    gridManager.AddConnectedTiles(this.gameObject);

                    lr.SetPosition(2, new Vector3(col.gameObject.transform.localPosition.x, col.gameObject.transform.localPosition.y, 0f));
                }
            }
        }


            

       
    }





    void OnMouseDown()
    {
      

        if ((spawnedLineObject == null) && colorIdentity != ColorEnum.NONE)
        {
            gridManager.AddConnectedTiles(this.gameObject);
            createLineObject();
        }
               
    }
  

    public ColorEnum getTileColorIdentity()
    {
        return colorIdentity;
    }

}
