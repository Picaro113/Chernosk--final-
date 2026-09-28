using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerDownHandler
{
    public int width;
    public int height;
    public Image image;

    Vector3 previousPostion;
    public void SetGridSquareSize(float size)
    {
        RectTransform rect = GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(size * width, size * height);
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        InventoryHand.i.PickUpItem(this);
        PickUp();
    }

    public void PickUp()
    {
        image.raycastTarget = false;
        previousPostion = transform.localPosition;
    }

    public void CancelMove()
    {
        transform.localPosition = previousPostion;
        Drop();
    }

    public void Drop()
    {
        image.raycastTarget = true;
        InventoryHand.i.DropHeldItem();
    }
}
