using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine.EventSystems;
public class InventoryItem : MonoBehaviour
{
    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private TextMeshProUGUI quantityText;

    [SerializeField]
    private Image borderImage;

    public event Action<InventoryItem> OnItemClicked, OnItemDropped, OnItemBeginDrag, OnItemEndDrag, OnLeftMouseBtnClick;

    private bool empty = true;

    public void Awake()
    {
        ResetData();

    }

    public void ResetData()
    {
        itemImage.gameObject.SetActive(false);
        empty = true;
    }

    public void Deselect()
    {
        borderImage.enabled = false;
    }

    public void SetData(Sprite sprite, int quantity)
    {
        itemImage.gameObject.SetActive(true);
        itemImage.sprite = sprite;
        quantityText.text = quantity.ToString();
        empty = false;
    }

    public void Select()
    {
        borderImage.enabled = true;
    }

    public void OnBeginDrag()
    {
        if (!empty)
        {
            OnItemBeginDrag?.Invoke(this);
        }
    }

    public void OnDrop()
    {
        OnItemDropped?.Invoke(this);
    }

    public void OnEndDrag()
    {
        OnItemEndDrag?.Invoke(this);
    }

    public void OnPointerClick(BaseEventData data)
    {
        PointerEventData pointerdata = (PointerEventData)data;
        if (pointerdata.button == PointerEventData.InputButton.Left)
        {
            OnLeftMouseBtnClick?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
        }

    }
}