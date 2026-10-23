using UnityEngine;
using UnityEngine.UI;
public abstract class ItemSquare : MonoBehaviour
{
    public Image image;
    public Color highlightColor;
    public Color invalidColor;
    Color defaultColor;
    private void Start()
    {
        defaultColor = image.color;
    }

    public abstract void DropItem();
    public abstract bool IsValidLocation(InventoryItem item);

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

    public abstract void ItemGridApplyState(int width, int height, GridSquareState toApply);


}
