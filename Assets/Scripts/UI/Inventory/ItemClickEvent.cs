using System;
using UnityEngine;

public static class ItemClickEvent
{
    public static event Action<Item> OnItemClicked;

    public static void ItemClicked(Item item)
    {
        OnItemClicked?.Invoke(item); // if there are any subscribers to the event, invoke them
    }
}
