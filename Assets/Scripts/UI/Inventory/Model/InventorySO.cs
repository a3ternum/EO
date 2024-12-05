using UnityEngine;
using System;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/Inventory")]
public class InventorySO : ScriptableObject
{
    [SerializeField]
    private List<OurInventoryItem> OurInventoryItems;

    [field: SerializeField]
    public int Size { get; private set; } = 10;

    public void Initialize()
    {
        OurInventoryItems = new List<OurInventoryItem>();
        for (int i = 0; i < Size; i++)
        {
            OurInventoryItems.Add(OurInventoryItem.GetEmptyItem());
        }
    }

    public void Additem(ItemSO item, int quantity)
    {
        for (int i = 0; i < OurInventoryItems.Count; i++)
        {
            if (OurInventoryItems[i].isEmpty)
            {
                OurInventoryItems[i] = new OurInventoryItem
                {
                    item = item,
                    quantity = quantity
                };
            }
        }
    }

    public Dictionary<int, OurInventoryItem> GetCurrenInventoryState()
    {
        Dictionary<int, OurInventoryItem> inventoryState = new Dictionary<int, OurInventoryItem>();
        for (int i = 0; i < OurInventoryItems.Count; i++)
        {
            if (!OurInventoryItems[i].isEmpty)
                inventoryState[i] = OurInventoryItems[i];
        }
        return inventoryState;
    }
}

[Serializable]
public struct OurInventoryItem
{
    public int quantity;
    public ItemSO item;
    public bool isEmpty => item == null;

    public OurInventoryItem ChangeQuantity(int newQuantity)
    {
        return new OurInventoryItem
        {
            item = item,
            quantity = newQuantity
        };
    }

    public static OurInventoryItem GetEmptyItem() => new OurInventoryItem
    {
        item = null,
        quantity = 0
    };


}