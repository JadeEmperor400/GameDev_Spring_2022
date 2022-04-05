using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public AudioSource audioSource;
    private ColorEnum colorIdentity;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private GameObject lineObjectPrefab;


    private GridManager gridManager; 
    private GameObject spawnedLineObject;

    private int xId; 
    private int yId;

    [SerializeField]
    private bool inUse = false; //flag to determine if its part of a connection already 

    private bool inUseLine = false; //flag to the determine if the LINE is part of a connection already 

    //linkedlist data structure technique to check if connection is valid
    [SerializeField]//showing it to the editor for debugging 
    private GameObject prevTile;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gridManager = FindObjectOfType<GridManager>();
    }

    private void Update()
    {
        if (spawnedLineObject == null)
        {
            inUseLine = false;
        }
        else
            inUseLine = true;
    }
    public void Init(ColorEnum colorType)
    {
        switch (colorType)
        {
            case ColorEnum.RED:
                colorIdentity = ColorEnum.RED;
                spriteRenderer.color = new Color( 1.0f, 0.3f, 0.3f);
                break;

            case ColorEnum.BLUE:
                colorIdentity = ColorEnum.BLUE;
                spriteRenderer.color = new Color(0.3f, 0.3f, 1.0f);
                break;

            case ColorEnum.GREEN:
                colorIdentity = ColorEnum.GREEN;
                spriteRenderer.color = new Color(0.3f, 1.0f, 0.3f); ;
                break;

            case ColorEnum.NONE:
                colorIdentity = ColorEnum.NONE;
                spriteRenderer.color = Color.black;
                break;
        }
    }
    private void OnMouseDown()
    {
        if ((spawnedLineObject == null) && colorIdentity != ColorEnum.NONE && (inUse == false) && !gridManager.Falling)
        {
            gridManager.AddConnectedTiles(this.gameObject);
            CreateLineObject();
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        //if placed on a tile that is already in use 
        if (col.gameObject.CompareTag("LineHead"))
        {
           
            if (inUse == true)
            {
               
                gridManager.RemoveLineObjectsInList(gridManager.getConnectedTiles()); //remove the lineobjects in the current connected tiles
            }
                
        }

            //if placed near a tile that is not a neighboring tile 
            if (col.gameObject.CompareTag("LineHead"))
        {
             if(!IsFromNeighborTile(col.gameObject))
             {
               
                gridManager.RemoveLineObjectsInList(gridManager.getConnectedTiles()); //remove the lineobjects in the current connected tiles
               return;
              }   
        }
        //if placed on a black or none colored tile 
        if (colorIdentity == ColorEnum.NONE)
        {          
            if (col.gameObject.CompareTag("LineHead"))
            {
             
                if(prevTile == null)
                {
                    prevTile = col.gameObject.transform.parent.gameObject.transform.parent.gameObject; //somehow this works...
                }

                SnapToPosition(col.gameObject);
                gridManager.AddConnectedTiles(this.gameObject);
                if (spawnedLineObject == null)
                {
                    CreateLineObject();
                }
            }                
        }
       

       //if placed next to a color tile
       if(colorIdentity != ColorEnum.NONE)
        {
            if(col.gameObject.CompareTag("LineHead"))
            {             
                var firstTile = gridManager.getFirstConnectedTile();
                if (firstTile == null)
                {
                   
                    return;
                }
                   

                if(GameObject.ReferenceEquals(firstTile, this.gameObject))
                {
                    return; //this is when the lineobject hits itself
                }


                if (firstTile.GetComponent<Tile>().GetTileColorIdentity() == GetTileColorIdentity())
                {
                    SnapToPosition(col.gameObject);
                    gridManager.AddConnectedTiles(this.gameObject);
                    //alert gridmanger about connection 
                    gridManager.ValidateConnection();
                }
                else
                {
                    gridManager.RemoveLineObjectsInList(gridManager.getConnectedTiles());
                }



            }
        } 


    }
    private void CreateLineObject()
    {
      
        spawnedLineObject = Instantiate(lineObjectPrefab, transform.position, Quaternion.identity);
        spawnedLineObject.name = name + " lineObject";
        spawnedLineObject.transform.parent = transform;

        var Line = spawnedLineObject.gameObject.GetComponentInChildren<Line>();

        if (Line != null)
            Line.SetLineID(GetXID(), GetYID());     
    }

    public void DestroyLineObject()
    {
       
        if (spawnedLineObject != null)
        {
            Destroy(spawnedLineObject);
        }
    }

    //simple solution, a neighboring tile is a tile that has x and y value within a range of 1 
    private bool IsFromNeighborTile(GameObject lineObject)
    {
        var Line = lineObject.GetComponent<Line>();
        if (Line != null)
        {
           int lnx = Line.GetLineXID();
           int lny = Line.GetLineYID();
            
            if ((lnx > GetXID() + 1) || (lnx < GetXID() - 1))
            {             
                
                return false;
            }
              
            if ((lny > GetYID() + 1) || (lny < GetYID() - 1))
            {               
                return false;
            }

           
            if ((lnx != GetXID() )  &&  (lny != GetYID()) )
            {
               
                return false;
            }
        } 
        else
        {
           
            return false;   
        }

       
        return true;
    }


    private void SnapToPosition(GameObject lineObject)
    {
        var lr = lineObject.gameObject.transform.parent.GetComponentInChildren<LineRenderer>();
        var Line = lineObject.gameObject.GetComponent<Line>();
        Line.DisableDrag();
        lineObject.gameObject.transform.position = transform.position;
        lr.SetPosition(2, new Vector3(lineObject.gameObject.transform.localPosition.x, lineObject.gameObject.transform.localPosition.y, 0f));
        if (audioSource == null)
        {
           
        }
        audioSource.Play(0);
    }

    //GETTERS AND SETTERS 
    public bool GetInUseLine()
    {
        return inUseLine;
    }
    public ColorEnum GetTileColorIdentity()
    {
        return colorIdentity;
    }
    public void SetTileColorIdentity(ColorEnum colorEnum)
    {
        Init(colorEnum);
    }

    public int GetXID()
    {
        return xId;
    }

    public int GetYID()
    {
        return yId;
    }

    public void SetTileId(int xId, int yId)
    {
        this.xId = xId;
        this.yId = yId;
    }

    public bool IsInUse()
    {
        return inUse;
    }

    public void SetInUse(bool boolParam)
    {
        inUse = boolParam;
    }

    public GameObject GetPrevTile()
    {
        return prevTile;
    }
}
