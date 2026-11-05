using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject dialogueObject;
    public InventoryToggler inventory;

    public void UpdateDialogueObject(DialogueObject dialogueObject)
    {
        this.dialogueObject = dialogueObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out FPSMovement player))
        {
            player.interactable = this;
            inventory.enabled = false;
            Debug.Log("Player Detected");
        }
        if (other.CompareTag("Bullet"))
        {
            Physics.IgnoreCollision(other, GetComponent<SphereCollider>());
            Debug.Log("Bullet Ignored"); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out FPSMovement player))
        {
            Debug.Log("Player Left");
            if (player.interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                inventory.enabled = true;
                player.interactable = null;
            }
        }
    }

    public void Interact(FPSMovement player)
    {
        foreach (DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>())
        {
            if (responseEvents.DialogueObject == dialogueObject)
            {
                player.DialogueUI.AddResponseEvents(responseEvents.Events);
                Debug.Log("Added Response Events", responseEvents); 
                break;
            }
        }
        player.DialogueUI.ShowDialogue(dialogueObject);
    }
}
 