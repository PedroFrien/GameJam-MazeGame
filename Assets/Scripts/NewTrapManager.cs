using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Trap
{
    [SerializeField] public string name;
    [SerializeField] public GameObject trapPrefab;
    [SerializeField] public Vector3 offset;
    [SerializeField] public float spawnWeight;
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
    [SerializeField] private TMP_Text hoverText;

    [SerializeField] private float hoverDistance;

    [SerializeField] private bool infinite = false;

    private Trap currentTrapStruct;

    private float gridSize;

    private string[] FigureTaps = new string[] { "FigureTap01", "FigureTap02" };
    // Start is called before the first frame update
    void Start()
    {
        hoverText.enabled = false;

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
        currentTrapStruct = FindTrapByName(trapName);
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

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Figurine"))
            {
                hoverText.enabled = true;
                Vector3 screenPosition = Input.mousePosition + new Vector3(0, hoverDistance, 0);
                hoverText.transform.position = screenPosition;


                if (hit.collider.gameObject.name == "SpikeTrapFigurine(Clone)")
                {
                    hoverText.text ="Spike Trap";
                }
                if (hit.collider.gameObject.name == "TemporaryWallFigurine(Clone)")
                {
                    hoverText.text = "Temporary Wall";
                }
                if (hit.collider.gameObject.name == "HeaterTrapFigurine(Clone)")
                {
                    hoverText.text = "Heater Trap";
                }
                if (hit.collider.gameObject.name == "HourglassFigurine(Clone)")
                {
                    hoverText.text = "Hourglass";
                }
                if (hit.collider.gameObject.name == "SawTrapFigurine(Clone)")
                {
                    hoverText.text = "Saw Trap";
                }
            }
            else
            {
                hoverText.enabled = false;
            }

            if (Input.GetMouseButtonDown(0))
            {
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
        Vector3 offset = new Vector3(trapCoordinate.x + currentTrapStruct.offset.x, trapCoordinate.y + (placingTrap.transform.localScale.y / 2 + currentTrapStruct.offset.y), trapCoordinate.z + currentTrapStruct.offset.z);
        var spawnedTrap = Instantiate(placingTrap, offset, Quaternion.identity);
        //spawnedTrap.transform.localScale = new Vector3(gridSize, spawnedTrap.transform.localScale.y, gridSize);
        spawnedTrap.transform.SetParent(trapCollection.transform);

        FindObjectOfType<AudioManager>().PlaySound("TrapPlaced", Camera.main.transform.position, spawnedTrap);

        if (infinite == false)
        {
            trapToPlace = null;
            sampleText.GetComponent<Text_SelectedTrap>().ChangeText("None");
            grid.SetActive(false);

            
        }
       
    }

    void PickUpTrap(GameObject figurine)
    {
        int randomIndex = Random.Range(0, FigureTaps.Length);
        FindObjectOfType<AudioManager>().PlaySound(FigureTaps[randomIndex], figurine.transform.position, figurine);

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
        if (figurine.name == "HourglassFigurine(Clone)")
        { 
            SelectTrap("HourGlass");
        }
        if (figurine.name == "SawTrapFigurine(Clone)")
        {
            SelectTrap("SawTrap");
        }
        Destroy(figurine);
    }
}
