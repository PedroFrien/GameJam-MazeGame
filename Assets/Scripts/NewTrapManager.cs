using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class NewTrapManager : MonoBehaviour
{
    public GameObject[] traps;
    [SerializeField] private GameObject GridManager;

    [SerializeField] private GameObject selectedTrap;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Tile"))
            {
                PlaceTrapAtPosition(hit.collider.transform.position);
            }
        }
    }

    void PlaceTrapAtPosition(Vector3 trapCoordinate)
    {
        Instantiate(selectedTrap, trapCoordinate, Quaternion.identity);
    }
}
