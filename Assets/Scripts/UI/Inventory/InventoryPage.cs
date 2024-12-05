using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using JetBrains.Annotations;

public class InventoryPage : MonoBehaviour
{
    [SerializeField]
    private InventoryItem itemPrefab;

    [SerializeField]
    private RectTransform contentPanel;

    [SerializeField]
    private InventoryDescription itemDescription;
    List<InventoryItem> listOfItems = new List<InventoryItem>();

    [SerializeField]
    private MouseFollower mouseFollower;

    public Sprite sprite, sprite2;
    public int quantity;
    public string title, description;

    private int currentlyDraggedItemIndex = -1; 

    private void Awake()
    {
        Hide();
        mouseFollower.Toggle(false);
        itemDescription.ResetDescription();
    }

    public void InitializeInventory(int inventorySize)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            InventoryItem item = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            item.transform.SetParent(contentPanel);
            listOfItems.Add(item);

            item.OnItemClicked += HandleItemSelection;
            item.OnItemBeginDrag += HandleBeginDrag;
            item.OnItemDropped += HandleSwap;
            item.OnItemEndDrag += HandleEndDrag;
            item.OnLeftMouseBtnClick += HandleShowItemActions;
        }
    }

    private void HandleBeginDrag(InventoryItem inventoryItem)
    {
        int index = listOfItems.IndexOf(inventoryItem);
        if (index == -1)
        {
            return;
        }
        currentlyDraggedItemIndex = index;

        mouseFollower.Toggle(true);
        mouseFollower.SetData(index == 0 ? sprite : sprite2, quantity);
    }

    private void HandleSwap(InventoryItem inventoryItem)
    {
        int index = listOfItems.IndexOf(inventoryItem);
        if (index == -1)
        {
            mouseFollower.Toggle(false);
            currentlyDraggedItemIndex = -1;
            return;
        }

        listOfItems[currentlyDraggedItemIndex].SetData(index == 0 ? sprite : sprite2, quantity);
        listOfItems[index].SetData(currentlyDraggedItemIndex == 0 ? sprite : sprite2, quantity);
        currentlyDraggedItemIndex = -1;
    }

    private void HandleEndDrag(InventoryItem inventoryItem)
    {
        mouseFollower.Toggle(false);

    }

    private void HandleItemSelection(InventoryItem inventoryItem)
    {
        itemDescription.SetDescription(sprite, title, description);
        listOfItems[0].Select();
    }

    private void HandleShowItemActions(InventoryItem inventoryItem)
    {
        
    }


    public void Show()
    {
        gameObject.SetActive(true);
        itemDescription.ResetDescription();

        listOfItems[0].SetData(sprite, quantity);
        listOfItems[1].SetData(sprite2, quantity);


    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }





}
