using Unity.VisualScripting;
using UnityEngine;

public class InventoryToggler : MonoBehaviour
{
    public GameObject Inventory;
    public bool isActive = false;
    public FPSMovement playerMovement;
    private void Start()
    {
        Inventory.gameObject.SetActive(false);
    }
    void Update()
    {
        Debug.Log("inventory is" + Inventory.activeSelf);
        InventoryEnabler();
    }

    public void InventoryEnabler()
    {
        if (isActive == false && Input.GetKeyDown(KeyCode.Tab))
        {
            Inventory.gameObject.SetActive(true);
            isActive = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;           
        }
        else if (isActive == true && Input.GetKeyDown(KeyCode.Tab))
        {
            Inventory.gameObject.SetActive(false);
            isActive = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;          
        }
    }
}
