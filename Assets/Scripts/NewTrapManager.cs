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
    [SerializeField] private GameObject playerController;
    [SerializeField] private GameObject trapCollection;

    [SerializeField] public string newSelectedTrap;

    [SerializeField] private GameObject nullTrap;

    private GameObject trapToPlace = null;

    [SerializeField] private GameObject sampleText;
    // Start is called before the first frame update
    void Start()
    {

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
        if (currentTrapStruct.gridSize == "1x1")
        {
            playerController.GetComponent<PlayerController>().SwitchDimensions("1x1");
        }
        if (currentTrapStruct.gridSize == "2x2")
        {
            playerController.GetComponent<PlayerController>().SwitchDimensions("2x2");
        }
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
        }
    }

    void PlaceTrapAtPosition(Vector3 trapCoordinate, GameObject placingTrap)
    {
        var spawnedTrap = Instantiate(placingTrap, trapCoordinate, Quaternion.identity);
        spawnedTrap.transform.SetParent(trapCollection.transform);
        trapToPlace = null;
        sampleText.GetComponent<Text_SelectedTrap>().ChangeText("None");
        grid.SetActive(false);
    }
}
