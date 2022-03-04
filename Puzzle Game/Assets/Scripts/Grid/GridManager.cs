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
    //TODO 

    private const int X_OFFSET = -2;
    private const int Y_OFFSET = -3;
    [SerializeField]
    private int _width, _height;

    [SerializeField]
    private Tile _tilePrefab;



    public List<GameObject> connectedTiles;


    void Start()
    {
        GenerateGrid(); //might be better to do it on awake?
    }


    void GenerateGrid()
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
            }
        }
    }
    
    public void AddConnectedTiles(GameObject tile)
    {
        if(tile.gameObject.GetComponent<Tile>().getTileColorIdentity() != ColorEnum.NONE)
        {
            if(connectedTiles.Count > 0)
            {
                if(tile.gameObject.GetComponent<Tile>().getTileColorIdentity() != connectedTiles[0].gameObject.GetComponent<Tile>().getTileColorIdentity())
                {
                    Debug.Log("you clicked on a tile, and then clicked on another tile of a different color type");
                    RemoveLineObjectsInList();
                    RemoveTilesInList();
                    connectedTiles.Add(tile);
                    return;
                }
            }
        }


       
        connectedTiles.Add(tile);
    }



    public GameObject getFirstConnectedTile()
    {
        if (connectedTiles.Count == 0)
        { return null; }


        return connectedTiles[0];
    }


    public void RemoveLineObjectsInList()
    {
        foreach (var tile in connectedTiles)
        {
            tile.gameObject.GetComponent<Tile>().destroyLineObject();
        }
    }

    public void RemoveTilesInList()
    {
        connectedTiles.Clear();
    }
    


}
