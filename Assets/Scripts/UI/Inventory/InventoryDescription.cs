using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class InventoryDescription : MonoBehaviour
{
    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private TextMeshProUGUI itemName;
    [SerializeField]
    private TextMeshProUGUI itemDescription;

    public void Awake()
    {
        ResetDescription();
    }

    public void ResetDescription()
    {
        this.itemImage.gameObject.SetActive(false);
        this.itemName.text = "";
        this.itemDescription.text = "";
    }

    public void SetDescription(Sprite sprite, string ItemName, string itemDescription)
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        this.itemName.text = ItemName;
        this.itemDescription.text = itemDescription;
    }

}
