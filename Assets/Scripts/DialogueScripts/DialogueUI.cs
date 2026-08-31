using UnityEngine;
using TMPro;
using System.Collections;
public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;

    public bool IsOpen { get; private set;  }

    private ResponseHandler responseHandler;

    [SerializeField] private PlayerMovement playerMovement;
    private TypeWriterEffect typeWriterEffect;
    private void Start()
    {       
       responseHandler = GetComponent<ResponseHandler>();
       typeWriterEffect = GetComponent<TypeWriterEffect>();
       CloseDialogueBox();
    }

    public void ShowDialogue (DialogueObject dialogueObject)
    {
        IsOpen = true;
        Debug.Log("Showing Dialogue Box");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerMovement.enabled = false;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        responseHandler.AddResponseEvents(responseEvents);
    }


    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
            for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
            {   
         
                string dialogue = dialogueObject.Dialogue[i];

                yield return RunTypingEffect(dialogue);

                textLabel.text = dialogue;

                if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;

                yield return null;

                yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            }

            if (dialogueObject.HasResponses)
            {
                responseHandler.ShowResponses(dialogueObject.Responses);
            }
            else
            {
                CloseDialogueBox();
        
            }
    }
    private IEnumerator RunTypingEffect(string dialogue)
    {
        typeWriterEffect.Run(dialogue, textLabel);
        while (typeWriterEffect.IsRunning)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                typeWriterEffect.Stop();
            }
        }
    }

    public void CloseDialogueBox()
    {
        Debug.Log("Disabling Dialogue Box");
        dialogueBox.SetActive(false);
        IsOpen = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerMovement.enabled = true;
        
        textLabel.text = string.Empty;
    }
}
