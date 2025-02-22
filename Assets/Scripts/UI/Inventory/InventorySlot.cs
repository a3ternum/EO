using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Threading;
using JetBrains.Annotations;

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

        // implement the logic for consumable items
        if (inventoryItem.IsDraggableItem() == false) // item was never draggable so immediately return
        {
            return;
        }

        if (CanConsumeItem(inventoryItem) == false) // item is not consumable
        {
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
        else // item is consumable
        {
            if (transform.childCount == 0) // inventory slot is empty
            {
                if (inventoryItem.IsRightMouseDragging()) // return consumable to original position
                {
                    inventoryItem.transform.SetParent(inventoryItem.parentAfterDrag);
                }
                else // user was left-mouse-dragging. Place the consumable in the slot
                {
                    inventoryItem.parentAfterDrag = transform;
                }
                
            }
            else // consumable is dropped onto another item
            {
                // check if the user was right mouse dragging
                if (inventoryItem.IsRightMouseDragging())
                {

                    // check if the item under the consumable is compatible
                    Transform currentItem = transform.GetChild(0);
                    InventoryItem currentItemComponent = currentItem.GetComponent<InventoryItem>();

                    if (currentItemComponent.item.consumableCompatible == true) // item is compatible with consumables
                    {
                        // consume the item
                        inventoryItem.item.UseItem(currentItemComponent.item);

                        // update the count of the consumable item
                        --inventoryItem.count;
                        Debug.Log("item used");
                        if (inventoryItem.count <= 0) // stack of consumable items is used up
                        {
                            inventoryItem.image.raycastTarget = false;
                            inventoryItem.transform.SetParent(null);
                            Destroy(inventoryItem.gameObject);
                        }
                        else // stack of consumables still has items
                        {
                            inventoryItem.refreshCount();
                        }
                    }
                    else // item in inventorySlot is not compatible with consumables
                    {
                        // return the item to original position
                        inventoryItem.transform.SetParent(inventoryItem.parentAfterDrag);
                    }
                }
                else // user was leftMouseDragging
                {
                    // swap the items
                    Transform currentItem = transform.GetChild(0);
                    InventoryItem currentItemComponent = currentItem.GetComponent<InventoryItem>();
                    
                    currentItem.SetParent(inventoryItem.parentAfterDrag);
                    currentItemComponent.parentAfterDrag = inventoryItem.parentAfterDrag;
                    inventoryItem.parentAfterDrag = transform;
                }
            }   

        }
    }

    private bool CanConsumeItem(InventoryItem inventoryItem)
    {
        return inventoryItem.item.type == ItemType.Consumable;
    }

}
