using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;

    [SerializeField] private GameObject _tilePrefab;

    [SerializeField] public float _gridSize;

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

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;

    //    for (int x = 0; x <= _width; x++)
    //    {
    //        Vector3 start = new Vector3(-2.5f * _gridSize + x * _gridSize, 0, -2.5f * _gridSize);
    //        Vector3 end = new Vector3(-2.5f * _gridSize + x * _gridSize, 0, (_height - 2.5f) * _gridSize);
    //        Gizmos.DrawLine(start, end);
    //    }

    //    // Adjust the starting position for z-axis lines
    //    for (int z = 0; z <= _height; z++)
    //    {
    //        Vector3 start = new Vector3(-2.5f * _gridSize, 0, -2.5f * _gridSize + z * _gridSize);
    //        Vector3 end = new Vector3((_width - 2.5f) * _gridSize, 0, -2.5f * _gridSize + z * _gridSize);
    //        Gizmos.DrawLine(start, end);
    //    }
    //}
}
