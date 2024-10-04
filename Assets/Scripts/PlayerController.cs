using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   

    public GameObject NewTrapManager;

    GameObject currentHoveredObject = null;
    private GameObject currentTrap;
    //int dimension2 = 1;

    [SerializeField] private Camera cameraPosition1;
    [SerializeField] private Camera cameraPosition2;

    [SerializeField] private float cameraSpeed = 1f;
    [SerializeField] private float moveThreshold = 0.1f;
    [SerializeField] private float cameraPanSpeed;

    private bool movementEnabled = true;
    [SerializeField] private bool topDown = false;

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

        Debug.Log(topDown);


        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!topDown)
            {
                StartCoroutine(MoveCamera(cameraPosition1.transform, cameraSpeed));
                Debug.Log("setting topDown to True");
                topDown = true;
            }

            else
            {
                StartCoroutine(MoveCamera(cameraPosition2.transform, cameraSpeed));
                topDown = false;
            }
        }



        if (topDown)
        {
            if (Input.GetKey(KeyCode.W))
            {
                Debug.Log("W");
                transform.Translate(0, cameraPanSpeed * Time.deltaTime, 0);
                // positive Z
            }

            if (Input.GetKey(KeyCode.S))
            {
                Debug.Log("S");
                transform.Translate(0, -cameraPanSpeed * Time.deltaTime, 0);
                // negative Z
            }

            if (Input.GetKey(KeyCode.D))
            {
                Debug.Log("D");
                transform.Translate(cameraPanSpeed * Time.deltaTime, 0, 0);
                // positive X
            }

            if (Input.GetKey(KeyCode.A))
            {
                Debug.Log("A");
                transform.Translate(-cameraPanSpeed * Time.deltaTime, 0, 0);
                // negative Z
            }
        }

    }

    //public void SwitchDimensions(string dimensions)
    //{
    //    if (dimensions == "1x1")
    //    {
    //        dimension2 = 1;
    //    }
    //    if (dimensions == "2x2")
    //    {
    //        dimension2 = 2;
    //    }
    //}

    public IEnumerator MoveCamera(Transform targetPosition, float cameraSpeed)
    {
        Debug.Log("MoveCameraCalled");
        if (!movementEnabled)
            yield break;


        movementEnabled = false;
        Debug.Log("MovementEnabled was true");
        while (Vector3.Distance(transform.position, targetPosition.position) > moveThreshold)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition.position, cameraSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetPosition.rotation, cameraSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition.position;
        transform.rotation = targetPosition.rotation;

        movementEnabled = true;
    }



    //public void ThrowItem(GameObject itemPrefab, float shootForce)
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hit;

    //    if (Physics.Raycast(ray, out hit))
    //    {
    //        GameObject thrownCube = Instantiate(itemPrefab, Camera.main.transform.position, Quaternion.identity);

    //        Vector3 direction = hit.point - Camera.main.transform.position;

    //        Rigidbody cubeRigidbody = thrownCube.GetComponent<Rigidbody>();

    //        if (cubeRigidbody != null)
    //        {

    //            cubeRigidbody.AddForce(direction.normalized * shootForce, ForceMode.Impulse);
    //        }
    //    }


    //}
}
