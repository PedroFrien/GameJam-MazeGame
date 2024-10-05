using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject highLight;
    [SerializeField] private Renderer objectRenderer;

    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material highlightMaterial;


    // Start is called before the first frame update

    public void Awake()
    {
        objectRenderer.enabled = false;
        objectRenderer.material = highlightMaterial;
    }
    public void ShowHighlight(bool isVisible)
    {
        if (isVisible == true)
        {
            //objectRenderer.material = highlightMaterial;
            objectRenderer.enabled = true;
        }
        if (isVisible == false)
        {
            objectRenderer.enabled = false;
        }
    }

    
}
