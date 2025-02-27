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
            if (transform.childCount == 0) // empty slot so change the parent of the item
            {
                inventoryItem.parentAfterDrag = transform;
            }
            else // slot is already occupied so we swap the items
            {
                // check if the item under the dragged item is the same type and stackable
                StackItems(inventoryItem);
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
                if (inventoryItem.IsRightMouseDragging()) // try to use the item
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
                    StackItems(inventoryItem);
                }
            }   

        }
    }

    private bool CanConsumeItem(InventoryItem inventoryItem)
    {
        return inventoryItem.item.type == ItemType.Consumable;
    }

    private void StackItems(InventoryItem inventoryItem)
    {
        // check if the item under the dragged item is the same type and stackable
        bool canStack = transform.GetChild(0).GetComponent<InventoryItem>().item == inventoryItem.item && inventoryItem.item.stackable;


        if (!canStack)
        {
            Transform currentItem = transform.GetChild(0);
            InventoryItem currentItemComponent = currentItem.GetComponent<InventoryItem>();

            // Swap the items
            currentItem.SetParent(inventoryItem.parentAfterDrag);
            currentItemComponent.parentAfterDrag = inventoryItem.parentAfterDrag;

            inventoryItem.parentAfterDrag = transform;
        }
        else
        {
            // stack the items
            InventoryItem currentItemComponent = transform.GetChild(0).GetComponent<InventoryItem>();
            int totalItems = currentItemComponent.count + inventoryItem.count;
            if (totalItems <= currentItemComponent.item.maxStackSize)
            {
                currentItemComponent.count = totalItems;
                currentItemComponent.refreshCount();
                inventoryItem.image.raycastTarget = false;
                inventoryItem.transform.SetParent(null);
                Destroy(inventoryItem.gameObject);
            }
            else
            {
                int itemsToMove = currentItemComponent.item.maxStackSize - currentItemComponent.count;
                currentItemComponent.count = currentItemComponent.item.maxStackSize;
                currentItemComponent.refreshCount();
                inventoryItem.count -= itemsToMove;
                inventoryItem.refreshCount();

                // return the remaining stack to the original position
                inventoryItem.transform.SetParent(inventoryItem.parentAfterDrag);
                inventoryItem.transform.localPosition = Vector3.zero;


            }

        }
    }
}
