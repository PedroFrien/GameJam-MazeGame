using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public int width, height;
    public float cellSize;
    private Vector3 originPosition;

    // Start is called before the first frame update


    public GridSystem(int width, int height, float cellSize, Vector3 originalPosition)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originalPosition;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;  // Set the color of the grid

        // Draw the grid lines
        for (int x = 0; x <= width; x++)
        {
            for (int y = 0; y <= height; y++)
            {
                // Horizontal lines
                Vector3 startHorizontal = GetWorldPosition(x, 0);
                Vector3 endHorizontal = GetWorldPosition(x, height);
                Gizmos.DrawLine(startHorizontal, endHorizontal);

                // Vertical lines
                Vector3 startVertical = GetWorldPosition(0, y);
                Vector3 endVertical = GetWorldPosition(width, y);
                Gizmos.DrawLine(startVertical, endVertical);
            }
        }
    }

        public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, 0, y) * cellSize + originPosition + new Vector3(cellSize / 2, 0, cellSize / 2);
    }

    public Vector2Int GetGridPosition(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        int y = Mathf.FloorToInt((worldPosition - originPosition).z / cellSize);
        return new Vector2Int(x, y);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
