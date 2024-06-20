using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    public ItemSlot[] itemSlot;

    public int AddItem(string itemName, int quantity, Sprite itemSprite)
    {
        Debug.Log("itemName = " + itemName + " quantity = " + quantity + " itemSprite = " + itemSprite);

        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (!itemSlot[i].isFull && (itemSlot[i].itemName == itemName || itemSlot[i].quantity == 0))
            {
                int leftOverItems = itemSlot[i].AddItem(itemName, quantity, itemSprite);
                if (leftOverItems > 0)
                    leftOverItems = AddItem(itemName, leftOverItems, itemSprite);
                return leftOverItems;
            }
        }
        return quantity;
    }

    public void RemoveUsedItem(string itemName)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].itemName == itemName)
            {
                itemSlot[i].quantity--;
                if (itemSlot[i].quantity <= 0)
                {
                    itemSlot[i].isFull = false;
                    itemSlot[i].itemName = "";
                    itemSlot[i].quantity = 0;
                    itemSlot[i].itemSprite = null;
                }
                UpdateSlotUI(i);
                break;
            }
        }
    }

    public bool HasItem(string itemName)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].itemName == itemName && itemSlot[i].quantity > 0)
            {
                return true;
            }
        }
        return false;
    }

    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }

    public void SaveInventory()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            PlayerPrefs.SetString("ItemName_" + i, itemSlot[i].itemName);
            PlayerPrefs.SetInt("ItemQuantity_" + i, itemSlot[i].quantity);
            if (itemSlot[i].isFull && itemSlot[i].itemSprite != null)
            {
                PlayerPrefs.SetString("ItemSprite_" + i, itemSlot[i].itemSprite.name);
            }
            else
            {
                PlayerPrefs.SetString("ItemSprite_" + i, "");
            }
        }
        PlayerPrefs.Save();
    }

    public void LoadInventory()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].itemName = PlayerPrefs.GetString("ItemName_" + i, "");
            itemSlot[i].quantity = PlayerPrefs.GetInt("ItemQuantity_" + i, 0);
            string spriteName = PlayerPrefs.GetString("ItemSprite_" + i, "");
            if (!string.IsNullOrEmpty(spriteName))
            {
                itemSlot[i].itemSprite = Resources.Load<Sprite>(spriteName);
                itemSlot[i].isFull = true;
            }
            else
            {
                itemSlot[i].itemSprite = null;
                itemSlot[i].isFull = false;
            }
            UpdateSlotUI(i);
        }
    }

    private void UpdateSlotUI(int slotIndex)
    {
        itemSlot[slotIndex].UpdateSlotUI();
    }
}
