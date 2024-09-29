using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

[System.Serializable]
public struct Trap
{
    [SerializeField] public string name;
    [SerializeField] public GameObject trapPrefab;
    [SerializeField] public string gridSize;
};

public class NewTrapManager : MonoBehaviour
{
    public Trap[] newTraps;
    public GameObject[] traps;
    [SerializeField] private GameObject GridManager;
    [SerializeField] private GameObject grid;

    [SerializeField] public string newSelectedTrap;

    [SerializeField] private GameObject nullTrap;

    private GameObject trapToPlace = null;

    [SerializeField] private GameObject sampleText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    GameObject FindTrapByName(string trapName)
    {
        foreach (Trap trap in newTraps)
        {
            if (trap.name == trapName)
            {
                return trap.trapPrefab;
            }
        }
        return nullTrap;
    }
    // Update is called once per frame

    public void SelectTrap(string trapName)
    {
        trapToPlace = FindTrapByName(trapName);
        sampleText.GetComponent<Text_SelectedTrap>().ChangeText(trapName);
        grid.SetActive(true);
    }
    void Update()
    {
        

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Tile"))
            {
                if (trapToPlace != null)
                {
                    PlaceTrapAtPosition(hit.collider.transform.position, trapToPlace);
                }
                
            }
        }
    }

    void PlaceTrapAtPosition(Vector3 trapCoordinate, GameObject trapToPlace)
    {
        Instantiate(trapToPlace, trapCoordinate, Quaternion.identity);
        trapToPlace = null;
        sampleText.GetComponent<Text_SelectedTrap>().ChangeText("None");
    }
}
