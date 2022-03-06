using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ColorEnum {
    RED, GREEN, BLUE, NONE
}


public class GridManager : MonoBehaviour
{    //TODO Tile script line 115 
    //TODO duplicate ColorEnum.NONE in list when hit Ontriggerenter2D in Tile Class
    //TODO establish connnection 
    //TODO automatic drag with mouse 
    //TODO consider using sets to deal with duplicates 
    //TODO stop using offsets, make it based on camera 
    //TODO for scaling, consider offsets based on size of tiles 
    //TODO in gridmanager, give specific amount of color amount and assign them  
    

    private const int X_OFFSET = -2;
    private const int Y_OFFSET = -3;

    [SerializeField]
    private int _width, _height;

    [SerializeField]
    private Tile _tilePrefab;

    private List<GameObject> allTilesInGrid = new List<GameObject>(); //private list must be initialized 
    public List<GameObject> connectedTiles;





    void Awake()
    {
        GenerateGrid(); 
    }

    void Start()
    {

    }
   private void GenerateGrid()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {

                var gridPlacement = new Vector3(x + X_OFFSET, y + Y_OFFSET);
                var spawnedTile = Instantiate(_tilePrefab, gridPlacement, Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.transform.parent = transform; //all tiles are now a child of the gridmanager object
                spawnedTile.transform.localScale += new Vector3(-0.1f, -0.1f, -0.1f);
                spawnedTile.Init();


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


    public GameObject getFirstConnectedTile()
    {
        if (connectedTiles.Count == 0)
        { 
            return null; 
        }

        return connectedTiles[0];
    }


    public void RemoveLineObjectsInList()
    {
        foreach (var tile in connectedTiles)
        {
            tile.gameObject.GetComponent<Tile>().DestroyLineObject();
        }
    }

    public void RemoveTilesInList()
    {
        connectedTiles.Clear();
    }
    


    public List<GameObject> getAllTilesInGrid()
    {
        return allTilesInGrid;
    }

}
