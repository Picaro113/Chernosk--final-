using JetBrains.Annotations;
using UnityEngine;

public class InventoryGrid : MonoBehaviour
{
    public int totalColumns;
    public int totalRows;
    public GameObject gridSquarePrefab;
    public float gridItemSize;
    public GameObject itemPrefab;
    GridSquare[,] grid;
    private void Start()
    {
        grid = new GridSquare[totalColumns, totalRows];
        for (int i = 0; i < totalRows; i++)
        {
            for(int j = 0; j < totalColumns; j++)
            {
                GameObject spawned = Instantiate(gridSquarePrefab, transform);
                spawned.transform.localPosition = new Vector3(GetX(j), GetY(i), 0);
                GridSquare gridSquare = spawned.GetComponent<GridSquare>();
                gridSquare.inventory = this;
                gridSquare.row = i;
                gridSquare.col = j;
                grid[j, i] = gridSquare;
            }
        }
        GameObject itemObject = Instantiate(itemPrefab, transform);
        itemObject.GetComponent<InventoryItem>().SetGridSquareSize(gridItemSize);
        SetItemPosition(itemObject.transform, 1, 2);
    }   

    float GetX(int column)
    {
        return column * gridItemSize;
    }
    float GetY(int row)
    {
        return row * gridItemSize;
    }

    void SetItemPosition(Transform item, int column, int row)
    {
        item.transform.localPosition = new Vector3(GetX(column), GetY(row), 0);
    }

    public void PlaceItem(InventoryItem item, int column, int row)
    {
        SetItemPosition(item.transform, column, row);
    }

    public bool IsValidLocation(int column, int row, int width, int height)
    {
        if (row + height - 1 >= totalRows)
        {
            return false;
        }
        if (column + width - 1 >= totalColumns)
        {
            return false; 
        }

        return true;
    }

    public void highlightItemGridHighlight(int column, int row, int width, int height)
    {
        for(int i = 0; i < totalRows; i++)
        {
            for (int j = 0; j < totalColumns; j++)
            {
                if (j >= column && j < column + width &&
                       i >= row && i < row + height)
                {
                    grid[j,i].SetHighlighted(true);
                }
                else
                {
                    grid[j,i].SetHighlighted(false);
                }
            }
        }
    }
}
