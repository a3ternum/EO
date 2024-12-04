using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class InventoryPage : MonoBehaviour
{
    [SerializeField]
    private InventoryItem itemPrefab;

    [SerializeField]
    private RectTransform contentPanel;

    [SerializeField]
    private InventoryDescription itemDescription;
    List<InventoryItem> listOfItems = new List<InventoryItem>();

    public Sprite sprite;
    public int quantity;
    public string title, description;


    private void Awake()
    {
        Hide();
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
        itemDescription.SetDescription(sprite, title, description);
        listOfItems[0].Select();
    }

    private void HandleShowItemActions(InventoryItem obj)
    {
        Debug.Log("Show item actions");
    }


    public void Show()
    {
        gameObject.SetActive(true);
        itemDescription.ResetDescription();

        listOfItems[0].SetData(sprite, quantity);
        
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }





}
