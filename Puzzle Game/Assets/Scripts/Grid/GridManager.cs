using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ColorEnum {
    RED, GREEN, BLUE, NONE
}


public class GridManager : MonoBehaviour
{  
    //TODO duplicate ColorEnum.NONE in list when hit Ontriggerenter2D in Tile Class
    //TODO establish connnection 
    //TODO automatic drag with mouse 
    //TODO consider using sets to deal with duplicates 
    //TODO for scaling, consider offsets based on size of tiles 
    //TODO in gridmanager, give specific amount of color amount and assign them  
    // 2 different buttons, one for clearing grid, and one for ending turn (resets grid to a new one)

    

    [SerializeField]
    private int width, height;

    [SerializeField]
    private Tile _tilePrefab;

    private List<GameObject> allTilesInGrid = new List<GameObject>(); //private list must be initialized, reference to all tiles in the grid 
    public List<GameObject> connectedTiles; //currently connected tiles 





    void Awake()
    {
        GenerateGrid(); 
    }

    void Start()
    {

    }
   private void GenerateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {

                var gridPlacement = new Vector3(x, y);
                var spawnedTile = Instantiate(_tilePrefab, gridPlacement, Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.transform.parent = transform; //all tiles are now a child of the gridmanager object
                spawnedTile.transform.localScale += new Vector3(-0.1f, -0.1f, -0.1f);


                spawnedTile.Init();
                spawnedTile.SetTileId(x, y);

                allTilesInGrid.Add(spawnedTile.gameObject);
                
              
            }
        }
    }
    
  

    public void AddConnectedTiles(GameObject tile)
    {
        CheckSameSelection(tile);
        if (tile.gameObject.GetComponent<Tile>().GetTileColorIdentity() != ColorEnum.NONE)
        {
            if(connectedTiles.Count > 0)
            {
                if(tile.gameObject.GetComponent<Tile>().GetTileColorIdentity() != connectedTiles[0].gameObject.GetComponent<Tile>().GetTileColorIdentity())
                {
                   
                    RemoveLineObjectsInList();
                    RemoveTilesInList();
                  
                    connectedTiles.Add(tile);
                    return;
                }
            }
        }
        connectedTiles.Add(tile);
    }


    //A utility check for if the player chooses a colored tile of the same color somewhere else 
    public void CheckSameSelection(GameObject tile)
    {
        if (connectedTiles.Count > 0)
        {
            if (tile.gameObject.GetComponent<Tile>().GetTileColorIdentity() == connectedTiles[0].gameObject.GetComponent<Tile>().GetTileColorIdentity())
            {
               // RemoveLineObjectsInList();
              //  RemoveTilesInList();

            }

        }

    }


    public void ValidateConnection()
    {
        for (int i = connectedTiles.Count- 2; i >= 1; i--)
        {         
            if(connectedTiles[i].gameObject.GetComponent<Tile>().GetPrevTile() == null)
            {
                Debug.Log("Invalid Connection");
                return;
            }
        }


        foreach (var tile in connectedTiles)
        {
            tile.gameObject.GetComponent<Tile>().SetInUse(true);
        }
        Debug.Log("Connection made");
    }

    public GameObject getFirstConnectedTile()
    {
        if (connectedTiles.Count == 0)
        { 
            return null; 
        }

        return connectedTiles[0];
    }


    public void RemoveLineObjectsInList()//add bool parameter to delete based on currently used nodes? 
    {
        foreach (var tile in connectedTiles)
        {

            if(tile.gameObject.GetComponent<Tile>().IsInUse() == false)
            tile.gameObject.GetComponent<Tile>().DestroyLineObject();
        }
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

}
