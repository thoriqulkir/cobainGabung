using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    // ===== ITEM DATA ===== //
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;

    [SerializeField]
    private int maxNumberOfItems;

    // ===== ITEM SLOT ===== //
    [SerializeField]
    private TMP_Text itemText;

    [SerializeField]
    private TMP_Text quantityText;

    [SerializeField]
    private Image itemImage;

    public GameObject selectedShader;
    public bool thisItemSelected;

    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        UpdateSlotUI();
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite)
    {
        if (isFull)
            return quantity;

        this.itemName = itemName;
        this.itemSprite = itemSprite;

        // Enable Text
        itemText.enabled = true;
        itemText.text = this.itemName; // Ensure itemName is correctly set

        // Enable Image
        itemImage.enabled = true;
        itemImage.sprite = itemSprite;

        this.quantity += quantity;
        if (this.quantity >= maxNumberOfItems)
        {
            quantityText.text = maxNumberOfItems.ToString();
            quantityText.enabled = true;
            isFull = true;

            // Return Sisa Item
            int extraItems = this.quantity - maxNumberOfItems;
            this.quantity = maxNumberOfItems;

            return extraItems;
        }

        quantityText.text = this.quantity.ToString();
        quantityText.enabled = true;
        itemText.text = this.itemName;
        itemText.enabled = true;

        return 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    public void OnLeftClick()
    {
        inventoryManager.DeselectAllSlots();
        selectedShader.SetActive(true);
        thisItemSelected = true;
    }

    public void OnRightClick()
    {
        // Create a new item
        // GameObject itemToDrop = new GameObject(itemName);
        // Item newItem = itemToDrop.AddComponent<Item>();
    }

    public void UpdateSlotUI()
    {
        if (isFull)
        {
            itemText.enabled = true;
            itemText.text = this.itemName;
            itemImage.enabled = true;
            itemImage.sprite = this.itemSprite;
            quantityText.enabled = true;
            quantityText.text = this.quantity.ToString();
        }
        else
        {
            itemText.enabled = false;
            itemImage.enabled = false;
            quantityText.enabled = false;
        }
    }
}
