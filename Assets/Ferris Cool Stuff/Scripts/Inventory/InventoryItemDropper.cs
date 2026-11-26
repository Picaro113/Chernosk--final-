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
        item.OnRemovedFromInventory.Invoke();
        GameObject spawned = Instantiate(item.itemData.worldPrefab, transform.position, transform.rotation);
        Ammo ammo = item.GetComponent<Ammo>();
        if (ammo != null)
        {
            spawned.GetComponent<Ammo>().amount = ammo.amount;
        }
    }
}
