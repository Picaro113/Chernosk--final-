using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GridSquare : MonoBehaviour
{
    public int row;
    public int col;
    public InventoryGrid inventory;
    public Image image;
    public Color highlightColor;
    Color defaultColor;

    private void Start()
    {
        defaultColor = image.color;
    }
    public void DropItem()
    {
        if (InventoryHand.i.GetHeldItem() != null)
        {
            inventory.PlaceItem(InventoryHand.i.GetHeldItem(), col, row);
        }
    }
    public bool IsValidLocation(int width, int height)
    {
        return inventory.IsValidLocation(col, row, width, height);
    }

    public void SetHighlighted(bool isHighlighted)
    {
        if(isHighlighted)
            image.color = highlightColor;
        else
            image.color = defaultColor;
    }

    public void ItemGridHighlight(int width, int height)
    {
        inventory.highlightItemGridHighlight(col, row, width, height);
    }
}
