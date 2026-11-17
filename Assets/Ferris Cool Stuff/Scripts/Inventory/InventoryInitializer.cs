using UnityEngine;

public class InventoryInitializer : MonoBehaviour
{
    public InventoryGrid inventory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory.Initialize();
    }
}
