using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject highLight;
    [SerializeField] private Renderer objectRenderer;

    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material highlightMaterial;
    // Start is called before the first frame update
    

    public void ShowHighlight(bool isVisible)
    {
        if (isVisible == true)
        {
            objectRenderer.material = highlightMaterial;
        }
        if (isVisible == false)
        {
            objectRenderer.material = defaultMaterial;
        }
    }
}
