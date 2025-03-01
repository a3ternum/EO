using System;
using System.Threading;
using UnityEngine;

public static class ItemClickEvent
{
    public static event Action<Item, int> OnItemClicked;

    public static void ItemClicked(Item item, int stackSize)
    {
        OnItemClicked?.Invoke(item, stackSize); // if there are any subscribers to the event, invoke them
    }
}
