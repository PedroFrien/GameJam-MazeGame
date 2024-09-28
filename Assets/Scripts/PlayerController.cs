using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameObject currentHoveredObject = null;
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
}
