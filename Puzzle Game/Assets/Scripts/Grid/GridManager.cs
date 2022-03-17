using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ColorEnum {
    RED, GREEN, BLUE, NONE
}


public class GridManager : MonoBehaviour
{
    //TODO Dynamic Dropdown system -> maybe try manipulating grid based on x and y pos, (Seperate Script, TileMovement), "invisible" instantiated row up top.
    //^ algorhthm for droppinng tiles based on connection. Refactor gridManager? 
    //TODO random null reference sometimes ontriggerenter, cant remake bug on command
    public AudioSource audioSource;
    public AudioClip redClip;
    public AudioClip blueClip;
    public AudioClip greenClip;

    [SerializeField]
    private GridComboManager gridComboManager;

    [SerializeField]
    [Range(12, 20)]
    private int RedPercentage = 12, BluePercentage = 12, GreenPercentage =12; 

    [SerializeField]
    [Range(5, 8)]
    private int width, height;
  

    [Range(2.5f, 4.5f)]
    public float offset = 3.5f;

    [SerializeField]
    private Tile _tilePrefab;

    [SerializeField]
    private List<GameObject> allTilesInGrid = new List<GameObject>(); //private list must be initialized, reference to all tiles in the grid 
    public List<GameObject> connectedTiles; //currently connected tiles 

    void Awake()
    {
        GenerateGrid(); 
        audioSource = GetComponent<AudioSource>();
    }

   private void GenerateGrid()
    {
        allTilesInGrid.Clear();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
              
                var gridPlacement = new Vector3(x * offset, y *offset);
                var spawnedTile = Instantiate(_tilePrefab, gridPlacement, Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.transform.parent = transform; //all tiles are now a child of the gridmanager object
                spawnedTile.transform.localScale += new Vector3(-0.1f, -0.1f, -0.1f);


                spawnedTile.Init(GenerateRandomColorType());
                spawnedTile.SetTileId(x, y);

                allTilesInGrid.Add(spawnedTile.gameObject);            
            }
        }

        ForceTilesToRange(3, 5);
    }


    public void RegenerateGrid()
    {
        foreach (var tile in allTilesInGrid)
        {
            if(tile.gameObject != null)
            Destroy(tile.gameObject);
        }
        connectedTiles = new List<GameObject>();
        GenerateGrid();
        gridComboManager.ClearCombo();
       
    }

    private ColorEnum GenerateRandomColorType()
    {
        int num = UnityEngine.Random.Range(1, 101);
       
        int none = 100 - (RedPercentage + BluePercentage + GreenPercentage);
     
        if (num <= none)
            return ColorEnum.NONE;
        else if (num <= RedPercentage + none)
            return ColorEnum.RED;
        else if (num <= BluePercentage + RedPercentage + none)
            return ColorEnum.BLUE;
        else if (num <= GreenPercentage + BluePercentage + RedPercentage + none) 
            return ColorEnum.GREEN;

        return ColorEnum.NONE;
    }

    public void RegenerateGridColors()//assumption that grid size will be the same, maybe add parameters for grid size 
    {
       
        
        
        //delete all line objects in the grid 
        RemoveLineObjectsInList(allTilesInGrid, true);

        //rechange the "type" of all the tiles 
        ReassignColorTypeInGrid(allTilesInGrid); 

      
    }

    private void ReassignColorTypeInGrid(List<GameObject> list)
    {
        foreach (var tile in list)
        {
            if(tile.gameObject != null)
            tile.GetComponent<Tile>().Init(GenerateRandomColorType());
        }
            
    }


    public void AddConnectedTiles(GameObject tile)
    {
        if (tile == null)
            Debug.Log("ay yo, tile is null"); 

      

        if (tile.gameObject.GetComponent<Tile>().GetTileColorIdentity() != ColorEnum.NONE)
        {
            if(connectedTiles.Count > 0) //this should be removed and implemented elsewhere
            {
                if (connectedTiles[0] == null)
                    return;
                //if the currently selected tile is not the same type as the first tile selected
                if (tile.gameObject.GetComponent<Tile>().GetTileColorIdentity() != connectedTiles[0].gameObject.GetComponent<Tile>().GetTileColorIdentity()) 
                {
                   
                    RemoveLineObjectsInList(connectedTiles);
                    RemoveTilesInList();
                  
                    connectedTiles.Add(tile);
                    return;
                }
            }
        }
        connectedTiles.Add(tile);
    }


    public void ValidateConnection()
    {
        for (int i = connectedTiles.Count- 2; i >= 1; i--)
        {         
            if(connectedTiles[i].gameObject.GetComponent<Tile>().GetPrevTile() == null)
            {
                Debug.Log("Invalid Connection");
                RemoveLineObjectsInList(connectedTiles);
                connectedTiles.Clear();
                return;
            }
        }
        foreach (var tile in connectedTiles)
        {
            tile.gameObject.GetComponent<Tile>().SetInUse(true);
        }

        gridComboManager.AddToCombo(connectedTiles);
        PlayConnectionSound(connectedTiles[0].gameObject.GetComponent<Tile>().GetTileColorIdentity());
        connectedTiles.Clear ();
        
    }

   

    public void RemoveLineObjectsInList(List<GameObject> list, bool completeRemove = false)
    {
        if (completeRemove == true)
        {
            foreach (var tile in list)
            {
                if(tile.gameObject != null)
                tile.gameObject.GetComponent<Tile>().DestroyLineObject();
            }
               
        }
        else
        {
            foreach (var tile in list)
            {
                if (tile.gameObject != null)
                {
                    if (tile.gameObject.GetComponent<Tile>().IsInUse() == false)
                        tile.gameObject.GetComponent<Tile>().DestroyLineObject(); //duplicated code, refactor later 
                }
                    
            }
        }
        connectedTiles.Clear(); 
    }

    private void ForceTilesToRange(int minAmount, int maxAmount)
    {
        ForceMinTiles(minAmount, ColorEnum.RED);
        ForceMinTiles(minAmount, ColorEnum.GREEN);
        ForceMinTiles(minAmount, ColorEnum.BLUE);
        ForceMaxTiles(maxAmount, ColorEnum.RED);
        ForceMaxTiles(maxAmount, ColorEnum.GREEN);
        ForceMaxTiles(maxAmount, ColorEnum.BLUE);
    }
    private void ForceMinTiles(int minAmount, ColorEnum colorType)
    {
        int amtOfColorTile = 0;
        foreach (var tile in allTilesInGrid)
        {
            if (tile.gameObject.GetComponent<Tile>().GetTileColorIdentity() == colorType) 
                amtOfColorTile++;
        }


        while (amtOfColorTile <= minAmount)
        {
           
            List<GameObject> blackTiles = new List<GameObject>(); 
            foreach(var tile in allTilesInGrid) //get all black tiles 
            {
                if (tile.gameObject.GetComponent<Tile>().GetTileColorIdentity() == ColorEnum.NONE) 
                    blackTiles.Add(tile);
            }

            //randomly change one of them to that color 
            int randNum = UnityEngine.Random.Range(0, blackTiles.Count -1);
            blackTiles[randNum].gameObject.GetComponent<Tile>().SetTileColorIdentity(colorType);  


            amtOfColorTile++;          
        }
    }
    private void ForceMaxTiles(int maxAmount, ColorEnum colorType)
    {
        int amtOfColorTile = 0;
        foreach (var tile in allTilesInGrid)
        {
            if (tile.gameObject.GetComponent<Tile>().GetTileColorIdentity() == colorType)
                amtOfColorTile++;
        }


        while (amtOfColorTile >= maxAmount)
        {

            List<GameObject> blackTiles = new List<GameObject>();
            foreach (var tile in allTilesInGrid) //get all black tiles 
            {
                if (tile.gameObject.GetComponent<Tile>().GetTileColorIdentity() == colorType)
                    blackTiles.Add(tile);
            }

            //randomly change one of them to black
            int randNum = UnityEngine.Random.Range(0, blackTiles.Count - 1);
            blackTiles[randNum].gameObject.GetComponent<Tile>().SetTileColorIdentity(ColorEnum.NONE); 


            amtOfColorTile--;
        }
    }

    private void PlayConnectionSound(ColorEnum colorEnum)
    {
        switch(colorEnum)
        {
            case ColorEnum.RED:
                audioSource.clip = redClip;
                audioSource.Play();
                break;
            case ColorEnum.BLUE:
                audioSource.clip = blueClip;
                audioSource.Play();
                break;
            case ColorEnum.GREEN:
                audioSource.clip = greenClip;
                audioSource.Play();
                break;
                default:
                Debug.Log("Play connection sound in GridManager");
                break;
        }
    }

    //GETTERS AND SETTERS
    public GameObject getFirstConnectedTile()
    {
        if (connectedTiles.Count == 0)
        {
            return null;
        }

        return connectedTiles[0];
    }

    public void RemoveTilesInList()
    {
        connectedTiles.Clear();
    }  

    public int getWidth()
    {
        return width;
    }
    public int getHeight()
    {
        return height;
    }

    public List<GameObject> getAllTilesInGrid()
    {
        return allTilesInGrid;
    }

    public List<GameObject> getConnectedTiles()
    {
        return connectedTiles;
    }

    public void SetGridOffset(float newOffset)
    {
        offset = newOffset;
    }

    public void SetGridWidth(int newWidth)
    {
        width = newWidth;
    }
    public void SetGridHeight(int newHeight)
    {
        height = newHeight;
    }

}
