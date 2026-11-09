using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerDownHandler
{
    public ItemData itemData;
    public int width;
    public int height;
    public Image image;

    public int Height
    {
        get
        {
            if(isRotated == false)
            {
                return width;
            }
            return height;
        }
    }

    public int Width
    {
        get
        {
            if (isRotated == false)
            {
                return height;
            }
            return width;
        }
    }

    public bool isRotated = false;

    int previousColumn;
    int previousRow;
    public void SetGridSquareSize(float size)
    {
        RectTransform rect = GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(size * width, size * height);
    }

    public void SetSize(float boxWidth, float boxHeight)
    {
        RectTransform rect = GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(boxWidth, boxHeight);
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        PickUp();
        InventoryHand.i.PickUpItem(this);
    }

    public void PickUp()
    {
        image.raycastTarget = false;
        InventoryContent content = InventoryGrid.instance.GetItemContent(this);
        if (content != null)
        {
            previousColumn = content.column;
            previousRow = content.row;            
        }
    }

    public void CancelMove()
    {
        InventoryGrid.instance.AddItemAtLocation(this, previousColumn, previousRow);
        Drop();
    }

    public void Drop()
    {
        image.raycastTarget = true;
        InventoryHand.i.DropHeldItem();
    }

    internal void Rotate()
    {
        isRotated = !isRotated;

        RectTransform rect = GetComponent<RectTransform>();
        rect.rotation = Quaternion.Euler(0, 0, isRotated == true ? 90f : 0f);
    }
}
