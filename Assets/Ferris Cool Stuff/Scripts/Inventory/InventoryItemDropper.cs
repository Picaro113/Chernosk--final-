using UnityEngine;

public class InventoryItemDropper : MonoBehaviour
{
    public static InventoryItemDropper instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    public void Drop(InventoryItem item)
    {
        Instantiate(item.itemData.worldPrefab, transform.position, transform.rotation);
    }
  
}
