using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   

    public GameObject NewTrapManager;

    GameObject currentHoveredObject = null;
    private GameObject currentTrap;
    int dimension2 = 1;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hoveredObject = hit.collider.gameObject;
            if (hoveredObject != currentHoveredObject && currentHoveredObject != null)
            {
                currentHoveredObject.GetComponent<Tile>().ShowHighlight(false);
            }
            if (hoveredObject.CompareTag("Tile")) 
            {
                currentHoveredObject = hoveredObject;
                hoveredObject.GetComponent<Tile>().ShowHighlight(true);
                
            }
            
        }


    }

    public void SwitchDimensions(string dimensions)
    {
        if (dimensions == "1x1")
        {
            dimension2 = 1;
        }
        if (dimensions == "2x2")
        {
            dimension2 = 2;
        }
    }
}
