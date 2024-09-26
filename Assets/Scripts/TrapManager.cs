using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour
{
    public GameObject[] traps;           // Array of trap prefabs
    private GridSystem gridSystem;       // The grid system handling grid positioning
    //private ConveyorBeltSystem conveyor; // Reference to conveyor belt system
    public GameObject selectedTrap;     // The currently selected trap prefab

    void Start()
    {
        // Initialize the grid system and conveyor belt
        gridSystem = FindObjectOfType<GridSystem>();
        //conveyor = GetComponent<ConveyorBeltSystem>(); // Reference to conveyor belt system
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left-click to place trap
        {
            PlaceTrapAtMousePosition();
        }

        // Update selected trap from conveyor belt
        //selectedTrap = conveyor.GetSelectedTrap();

    }

    void PlaceTrapAtMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Get grid position based on the mouse click
            Vector2Int gridPos = gridSystem.GetGridPosition(hit.point);

            // Check if within grid bounds
            if (gridPos.x >= 0 && gridPos.x < gridSystem.width && gridPos.y >= 0 && gridPos.y < gridSystem.height)
            {
                // Align to grid position
                Vector3 trapPosition = gridSystem.GetWorldPosition(gridPos.x, gridPos.y);

                // Check for existing traps at this position
                // Use a small box size to check around the trap position
                float checkSize = 0.5f; // Adjust this to match your trap size
                Collider[] colliders = Physics.OverlapBox(trapPosition, new Vector3(checkSize, 0.1f, checkSize), Quaternion.identity);

                // If no colliders are found, place the trap
                if (colliders.Length == 0)
                {
                    Instantiate(selectedTrap, trapPosition, Quaternion.identity);
                }
                else
                {
                    Debug.Log("Cannot place trap here, space is occupied.");
                }
            }
        }
    }
}
