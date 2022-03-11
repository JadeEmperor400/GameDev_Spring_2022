using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ColorEnum {
    RED, GREEN, BLUE, NONE
}


public class GridManager : MonoBehaviour
{
    //Debug build for sofi, to get more precise inspiration, add input system so she can change the size at will, maybe change node size, line renderer size etc.
    //if debug build works, then make one for the general public 

    //TODO random null reference sometimes ontriggerenter
    //Ignore diagonal connections  TESTING
    //TODO automatic drag with mouse, TESTING  
    //TODO 2 valid connections of the same color type causes a bug, fix should just be clear connectedtiles on valid connection

    //TODO consider using sets to deal with duplicates in connected tiles script , NOT NEEDED 

    //Seperate Class to measure combos and types of connections 


    [SerializeField]
    [Range(12, 20)]
    private int RedPercentage = 12, BluePercentage = 12, GreenPercentage =12; 

    [SerializeField]
    [Range(4, 12)]
    private int width, height;

  

    [Range(1f, 2.1f)]
    public float offset = 1f;

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
    }


    public void RegenerateGrid()
    {
        foreach (var tile in allTilesInGrid)
        {
            if(tile.gameObject != null)
            Destroy(tile.gameObject);
        }
        GenerateGrid();
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

    public void RegenerateGridColors()//assumption that grid size will be the same, maybe add parameters for grid size 
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
        {
            if(tile.gameObject != null)
            tile.GetComponent<Tile>().Init(GenerateRandomColorType());
        }
            
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
                RemoveLineObjectsInList(connectedTiles);
                connectedTiles.Clear();
                return;
            }
        }
        foreach (var tile in connectedTiles)
        {
            tile.gameObject.GetComponent<Tile>().SetInUse(true);
        }
        connectedTiles.Clear ();
        Debug.Log("Connection made");
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

                if (tile.gameObject.GetComponent<Tile>().IsInUse() == false)
                    tile.gameObject.GetComponent<Tile>().DestroyLineObject(); //duplicated code, refactor later 
            }
        }
        connectedTiles.Clear(); 
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
