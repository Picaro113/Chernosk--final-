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
    public Color invalidColor;
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

    public void SetState(GridSquareState state)
    {
        switch (state)
        {
            case GridSquareState.normal:
                image.color = defaultColor;
                break;
            case GridSquareState.highlight:
                image.color = highlightColor;
                break;
            case GridSquareState.invalid:
                image.color = invalidColor;
                break;
        }
    }

    public void ItemGridApplyState(int width, int height, GridSquareState toApply)
    {
        inventory.ItemGridApplyState(col, row, width, height, toApply);
    }
}

public enum GridSquareState
{
    normal,
    highlight,
    invalid,
}
