using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurvedText : MonoBehaviour
{
    TMP_Text textMesh;

    public void Start()
    {
        textMesh = GetComponent<TMP_Text>();
        Circle();
    }

    private void Circle()
    {
        for (int i = 0; i < textMesh.textInfo.characterCount; i++)
        {
            textMesh.transform.rotation = Quaternion.Euler(0, 0, (360 / textMesh.textInfo.characterCount) * i);
        }
    }

}
