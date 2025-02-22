using System.ComponentModel;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }


    public int maxStackedItems = 4;
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


    public void AddItem(Item item)
    {
        if (item.stackable)
        {
            for (int i = 0; i < inventorySlots.Length; i++) // check if item is already in inventory and stack it
            {
                InventorySlot slot = inventorySlots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

                if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < maxStackedItems)
                {
                    itemInSlot.count++; // increase the count of the items in the slot
                    item.onFloor = false; // item is now in inventory
                    itemInSlot.refreshCount(); // refresh the count of the item in the UI
                    return;
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
