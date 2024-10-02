using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public struct Item
    {
        [SerializeField] public string name;
        [SerializeField] public GameObject itemPrefab;
        [SerializeField] public string effectSize;
    };

    public Item[] items;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Item FindItemByName(string itemName)
    {
        foreach (Item item in items)
        {
            if (item.name == itemName)
            {
                return item;
            }
        }
        return items[0];
    }
}
