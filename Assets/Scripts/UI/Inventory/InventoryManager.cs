using JetBrains.Annotations;
using System;
using System.ComponentModel;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }


    public InventorySlot[] inventorySlots;
    public GameObject InventoryItemPrefab;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void OnEnable()
    {
        ItemClickEvent.OnItemClicked += AddItem;
    }

    private void OnDisable()
    {
        ItemClickEvent.OnItemClicked -= AddItem;
    }


    public void AddItem(Item item, int stackSize = 1)
    {
        if (item.stackable)
        {
            if (stackSize == 1)
            {
                for (int i = 0; i < inventorySlots.Length; i++) // check if item is already in inventory and stack it
                {
                    InventorySlot slot = inventorySlots[i];
                    InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

                    if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < item.maxStackSize)
                    {
                        itemInSlot.count++; // increase the count of the items in the slot
                        item.onFloor = false; // item is now in inventory
                        itemInSlot.refreshCount(); // refresh the count of the item in the UI
                        return;
                    }
                }
            }

            if (stackSize > 1)
            {
                for (int i = 0; i < inventorySlots.Length; i++) // check if item is already in inventory and stack it
                {
                    InventorySlot slot = inventorySlots[i];
                    InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();


                    if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < item.maxStackSize)
                    {
                        // add items from the stack until the slot is full
                        // then search for another slot
                        int spacesLeft = item.maxStackSize - itemInSlot.count;
                        int itemsToAdd = Math.Min(spacesLeft, stackSize);

                        itemInSlot.count += itemsToAdd; // increase the count of the items in the slot        
                        stackSize -= itemsToAdd;

                        itemInSlot.refreshCount(); // refresh the count of the item in the UI
                        if (stackSize == 0)
                        {
                            return;
                        }

                    }
                }
                // the same item type is not yet in inventory
                // so we have to add a new item to the inventory ourselves
                for (int i = 0; i < inventorySlots.Length; i++)
                {
                    if (inventorySlots[i].transform.childCount == 0)
                    {
                        InventorySlot slot = inventorySlots[i];
                        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                        if (itemInSlot == null)
                        {
                            SpawnNewItem(item, slot);
                            slot.GetComponentInChildren<InventoryItem>().count = stackSize; // change the stackSize of the item
                            return;
                        }
                    }
                }
            }
        }

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].transform.childCount == 0)
            {
                InventorySlot slot = inventorySlots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                if (itemInSlot == null)
                {
                    SpawnNewItem(item, slot);
                    return;
                }
            }
        }
        
        return; // item not picked up, could implement certain logic after this point.
    }

    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(InventoryItemPrefab, slot.transform);

        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);
    }

}
