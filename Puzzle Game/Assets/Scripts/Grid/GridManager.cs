using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    private const int X_OFFSET = -2;
    private const int Y_OFFSET = -3;
    [SerializeField]
    private int _width, _height;

    [SerializeField]
    private Tile _tilePrefab;

    void Start()
    {
        GenerateGrid();
    }


    void GenerateGrid()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {

                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x + X_OFFSET, y + Y_OFFSET), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                 spawnedTile.transform.parent = transform; //all tiles are now a child of the gridmanager object
                  spawnedTile.transform.localScale += new Vector3(-0.1f, -0.1f, -0.1f);

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);
            }
        }
    }
    
}
