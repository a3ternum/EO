using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Image image;
    public Color selectedColor, notSelectedColor;


    private void Awake()
    {
        Deselect();
    }
    public void Select()
    {
        image.color = selectedColor;
    }
    public void Deselect()
    {
        image.color = notSelectedColor;
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        InventoryItem inventoryItem = dropped.GetComponent<InventoryItem>();

        if (transform.childCount == 0)
        {
            inventoryItem.parentAfterDrag = transform;
        }
        else // slot is already occupied so we swap the items
        {
            Transform currentItem = transform.GetChild(0); 
            InventoryItem currentItemComponent = currentItem.GetComponent<InventoryItem>();

            // Swap the items
            currentItem.SetParent(inventoryItem.parentAfterDrag);
            currentItemComponent.parentAfterDrag = inventoryItem.parentAfterDrag;

            inventoryItem.parentAfterDrag = transform;
        }
    }
}
