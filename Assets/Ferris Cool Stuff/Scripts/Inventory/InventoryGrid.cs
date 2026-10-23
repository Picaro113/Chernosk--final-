using JetBrains.Annotations;
using UnityEngine;
using System.Collections.Generic;

public class InventoryGrid : MonoBehaviour
{
    public static InventoryGrid instance;
    public int totalColumns;
    public int totalRows;
    public GameObject gridSquarePrefab;
    public float gridItemSize;
    public GameObject itemPrefab;
    GridSquare[,] grid;
    List<InventoryContent> contents = new();
    private void Start()
    {
        instance = this;
        grid = new GridSquare[totalColumns, totalRows];
        for (int i = 0; i < totalRows; i++)
        {
            for (int j = 0; j < totalColumns; j++)
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

        foreach(InventoryContent content in contents)
        {
            if(IsOverlapWithContent(column, row, width, height, content))
            {
                return false;
            }
        }

        return true;
    }

    bool IsOverlapWithContent(int column, int row, int width, int height, InventoryContent content)
    {
       if(row + height <= content.row)
            return false;
       if (column + width <= content.column)
            return false;
       if (content.column + content.Item.width <= column)
            return false;
       if (content.row + content.Item.height <= row)
            return false;
      
       
       return true;
    }

    public void ItemGridApplyState(int column, int row, int width, int height, GridSquareState toApply)
    {
        for (int i = 0; i < totalRows; i++)
        {
            for (int j = 0; j < totalColumns; j++)
            {
                if (j >= column && j < column + width &&
                       i >= row && i < row + height)
                {
                    grid[j, i].SetState(toApply);
                }
                else
                {
                    grid[j, i].SetState(GridSquareState.normal);
                }
            }
        }
    }

    public void ClearHighlights()
    {
        for (int i = 0; i < totalRows; i++)
        {
            for (int j = 0; j < totalColumns; j++)
            {
                grid[j, i].SetState(GridSquareState.normal);
            }
        }
    }

    public bool AddItem(InventoryItem item)
    {
        for (int i = 0; i < totalRows; i++)
        {
            for (int j = 0; j < totalColumns; j++)
            {
                if (IsValidLocation(j, i, item.width, item.height))
                {
                    item.transform.parent = transform;
                    item.SetGridSquareSize(gridItemSize);
                    AddItemAtLocation(item, j, i);
                    return true;
                }
            }
        }
            return false;
    }

    public void AddItemAtLocation(InventoryItem item, int col, int row)
    {
        SetItemPosition(item.transform, col, row);

        InventoryContent content = new InventoryContent();
        content.Item = item;
        content.column = col;
        content.row = row;

        contents.Add(content);
    }

    public void RemoveItem(InventoryItem item)
    {
        foreach(InventoryContent content in contents)
        {
            if(content.Item == item)
            {
                contents.Remove(content);
                return;
            }
        }
    }
    public InventoryContent GetItemContent(InventoryItem item)
    {
        foreach (InventoryContent content in contents)
        {
            if (content.Item == item)
            {
               return content;
            }
        }

        return null;
    }
}
