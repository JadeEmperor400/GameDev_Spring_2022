using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ColorEnum {
    RED, GREEN, BLUE, NONE
}


public class GridManager : MonoBehaviour
{

    //TODO automatic drag with mouse 
    //TODO consider using sets to deal with duplicates in connected tiles script  
    //TODO for scaling, consider offsets based on size of tiles 
    //TODO 2 valid connections of the same color type causes a bug, fix should just be clear connectedtiles on valid connection

    [SerializeField]
    [Range(12, 20)]
    private int RedPercentage = 12, BluePercentage = 12, GreenPercentage =12; 

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


                spawnedTile.Init(GenerateRandomColorType());
                spawnedTile.SetTileId(x, y);

                allTilesInGrid.Add(spawnedTile.gameObject);            
            }
        }
    }

    private ColorEnum GenerateRandomColorType()
    {
        int num = UnityEngine.Random.Range(1, 101);
        int n = 100 - (RedPercentage + BluePercentage + GreenPercentage);

        if (num <= n)
            return ColorEnum.NONE;
        else if (num <= RedPercentage + n)
            return ColorEnum.RED;
        else if (num <= BluePercentage + RedPercentage + n)
            return ColorEnum.BLUE;
        else if (num <= GreenPercentage + BluePercentage + RedPercentage + n) //100 or less 
            return ColorEnum.GREEN;

        return ColorEnum.NONE;
    }

    public void RegenerateGrid()//assumption that grid size will be the same, maybe add parameters for grid size 
    {
        //delete all line objects in the grid 
        RemoveLineObjectsInList(allTilesInGrid, true);

        //rechange the "type" of all the tiles 
        ReassignColorTypeInGrid(allTilesInGrid); 

        //reset connected tiles, sanity check
        connectedTiles.Clear();
    }

    private void ReassignColorTypeInGrid(List<GameObject> list)
    {
        foreach (var tile in list)
            tile.GetComponent<Tile>().Init(GenerateRandomColorType());
    }


    public void AddConnectedTiles(GameObject tile)
    {
      
        if (tile.gameObject.GetComponent<Tile>().GetTileColorIdentity() != ColorEnum.NONE)
        {
            if(connectedTiles.Count > 0) //this should be removed and implemented elsewhere
            {
                //if the currently selected tile is not the same type as the first tile selected
                if(tile.gameObject.GetComponent<Tile>().GetTileColorIdentity() != connectedTiles[0].gameObject.GetComponent<Tile>().GetTileColorIdentity()) 
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
                return;
            }
        }
        foreach (var tile in connectedTiles)
        {
            tile.gameObject.GetComponent<Tile>().SetInUse(true);
        }
        Debug.Log("Connection made");
    }

   


    public void RemoveLineObjectsInList(List<GameObject> list, bool completeRemove = false)
    {
        if (completeRemove == true)
        {
            foreach (var tile in list)
                tile.gameObject.GetComponent<Tile>().DestroyLineObject();
        }
        else
        {
            foreach (var tile in list)
            {

                if (tile.gameObject.GetComponent<Tile>().IsInUse() == false)
                    tile.gameObject.GetComponent<Tile>().DestroyLineObject(); //duplicated code, refactor later 
            }
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

}
