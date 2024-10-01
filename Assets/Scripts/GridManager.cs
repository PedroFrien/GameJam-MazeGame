using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    [SerializeField] public int _width, _height;

    [SerializeField] private GameObject _tilePrefab;

    [SerializeField] private float _gridSize;

    [SerializeField] private GameObject grid;

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
                Vector3 spawnPosition = new Vector3(x * _gridSize, 0, z * _gridSize);

                

                var spawnedTile = Instantiate(_tilePrefab, spawnPosition, Quaternion.identity);

                spawnedTile.transform.localScale = new Vector3(_gridSize, 0.1f, _gridSize);
                spawnedTile.name = $"Tile {x} {z}";
                spawnedTile.transform.SetParent(grid.transform);
            }
        }
        grid.SetActive(false);
    }
}
