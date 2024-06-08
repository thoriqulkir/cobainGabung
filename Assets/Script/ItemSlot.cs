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
    private TMP_Text quantityText;

    [SerializeField]
    private Image itemImage;

    public GameObject selectedShader;
    public bool thisItemSelected;

    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }


    public int AddItem(string itemName, int quantity, Sprite itemSprite)
    {

        if (isFull)
            return quantity;

        this.itemName = itemName;
        this.itemSprite = itemSprite;


        //Enable Image
        itemImage.enabled = true;
        itemImage.sprite = itemSprite;

        this.quantity += quantity;
        if (this.quantity >= maxNumberOfItems)
        {
            quantityText.text = maxNumberOfItems.ToString();
            quantityText.enabled = true;
            isFull = true;

            //Return Sisa Item
            int extraItems = this.quantity - maxNumberOfItems;
            this.quantity = maxNumberOfItems;
            return extraItems;
        }

        quantityText.text = this.quantity.ToString();
        quantityText.enabled = true;

        return 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            onLeftClick();
        }

        if(eventData.button == PointerEventData.InputButton.Right)
        {
            onRightClick();
        }
    }


    public void onLeftClick()
    {
        inventoryManager.DeselectAllSlots();
        selectedShader.SetActive(true);
        thisItemSelected = true;
    }
    
    public void onRightClick()
    {
        //Create a new item
        //GameObject itemToDrop = new GameObject(itemName);
       // Item newItem = itemToDrop.AddComponent<Item>();
    }
}
