using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Grid : MonoBehaviour
{
    [SerializeField] private int _width, _height;

    [SerializeField] private GameObject _tilePrefab;

    [SerializeField] private float _gridSize;

    private void Start()
    {
        GenerateGrid();
    }
    // Start is called before the first frame update
    void GenerateGrid()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int z = 0; z < _height; z++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x, 0, z), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {z}";
            }
        }
    }
}
