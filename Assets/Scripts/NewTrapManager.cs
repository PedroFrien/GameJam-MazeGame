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
    [SerializeField] private GameObject gridManager;
    [SerializeField] private GameObject grid;
    [SerializeField] private GameObject playerController;
    [SerializeField] private GameObject trapCollection;

    [SerializeField] public string newSelectedTrap;

    [SerializeField] private GameObject nullTrap;

    private GameObject trapToPlace = null;

    [SerializeField] private GameObject sampleText;
    [SerializeField] private GameObject infiniteText;

    [SerializeField] private bool infinite = false;

    private float gridSize;
    // Start is called before the first frame update
    void Start()
    {
        gridSize = gridManager.GetComponent<GridManager>()._gridSize;
    }

    Trap FindTrapByName(string trapName)
    {
        foreach (Trap trap in newTraps)
        {
            if (trap.name == trapName)
            {
                return trap;
            }
        }
        return newTraps[0];
    }

    // Update is called once per frame

    public void SelectTrap(string trapName)
    {
        Trap currentTrapStruct = FindTrapByName(trapName);
        trapToPlace = currentTrapStruct.trapPrefab;
        sampleText.GetComponent<Text_SelectedTrap>().ChangeText(trapName);
        grid.SetActive(true);
        //if (currentTrapStruct.gridSize == "1x1")
        //{
        //    playerController.GetComponent<PlayerController>().SwitchDimensions("1x1");
        //}
        //if (currentTrapStruct.gridSize == "2x2")
        //{
        //    playerController.GetComponent<PlayerController>().SwitchDimensions("2x2");
        //}
    }
    void Update()
    {
        Debug.Log(trapToPlace);

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

            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Figurine"))
            {
                PickUpTrap(hit.collider.gameObject);
            }
        }

        if (Input.GetKeyDown("1"))
        {
            infinite = true;
            infiniteText.GetComponent<Text_Infinite>().ChangeText("Infinite");
        }
        if (Input.GetKeyDown("2"))
        {
            infiniteText.GetComponent<Text_Infinite>().ChangeText("Limited");
            infinite = false;
        }
    }

    void PlaceTrapAtPosition(Vector3 trapCoordinate, GameObject placingTrap)
    {
        Vector3 offset = new Vector3(trapCoordinate.x, trapCoordinate.y + (placingTrap.transform.localScale.y / 2), trapCoordinate.z);
        var spawnedTrap = Instantiate(placingTrap, offset, Quaternion.identity);
        //spawnedTrap.transform.localScale = new Vector3(gridSize, spawnedTrap.transform.localScale.y, gridSize);
        spawnedTrap.transform.SetParent(trapCollection.transform);
        
        if (infinite == false)
        {
            trapToPlace = null;
            sampleText.GetComponent<Text_SelectedTrap>().ChangeText("None");
            grid.SetActive(false);
        }
       
    }

    void PickUpTrap(GameObject figurine)
    {
        Debug.Log("PickUpTrap Called");
        if (figurine.name == "SpikeTrapFigurine(Clone)")
        {
            SelectTrap("SpikeTrap");
        }
        if (figurine.name == "TemporaryWallFigurine(Clone)")
        {
            SelectTrap("TemporaryWall");
        }
        if (figurine.name == "HeaterTrapFigurine(Clone)")
        {
            SelectTrap("HeaterTrap");
        }
        Destroy(figurine);
    }
}
