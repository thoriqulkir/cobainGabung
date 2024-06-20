using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName; // Make sure this is public
    public int quantity; // Make sure this is public
    public Sprite sprite; // Make sure this is public

    private InventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Item Name :" + itemName);
            int leftOverItems = inventoryManager.AddItem(itemName, quantity, sprite);
            if (leftOverItems <= 0) 
            {
                Destroy(gameObject);
            }
            else 
                quantity = leftOverItems;
        }
    }
}
