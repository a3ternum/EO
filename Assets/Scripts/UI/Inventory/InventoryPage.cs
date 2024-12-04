using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class InventoryPage : MonoBehaviour
{
    [SerializeField]
    private InventoryItem itemPrefab;

    [SerializeField]
    private RectTransform contentPanel;

    List<InventoryItem> listOfItems = new List<InventoryItem>();

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

    private void HandleBeginDrag(InventoryItem obj)
    {

    }

    private void HandleSwap(InventoryItem obj)
    {

    }

    private void HandleEndDrag(InventoryItem obj)
    {
    }

    private void HandleItemSelection(InventoryItem obj)
    {
        Debug.Log(obj.name);
    }

    private void HandleShowItemActions(InventoryItem obj)
    {
        Debug.Log("Show item actions");
    }


    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }





}
