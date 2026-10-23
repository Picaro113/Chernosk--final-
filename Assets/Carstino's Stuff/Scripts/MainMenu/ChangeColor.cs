using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class ChangeColor : MonoBehaviour
{
    public Color[] colors;
    public float duration;
    public GameObject sphere;
    private Renderer rend;
    private int currentColorIndex = 0;
    public bool onclick;

    private void Start()
    {
        rend = sphere.GetComponent<Renderer>();
        currentColorIndex = 0;
        rend.material.color = colors[currentColorIndex];
    }
    
    private void Update()
    {
        if (onclick == true)
        {
            StartCoroutine(ButtonManager(duration));
        }
        if (onclick == false)
        {
            StopAllCoroutines();
        }
    }

    public void changeColorAdd()
    {
        if (onclick == false)
        {
            if (currentColorIndex < colors.Length - 1)
            {
                currentColorIndex = currentColorIndex + 1;
                rend.material.color = colors[currentColorIndex];
                onclick = true;
            }
            else return;
        }
    }

    public void changeColorsMinus()
    {
        if (onclick == false)
        {
            if (currentColorIndex != 0)
            {
                currentColorIndex = currentColorIndex - 1;
                rend.material.color = colors[currentColorIndex];
                onclick = true;
            }
            else return;
        }
    }

    public IEnumerator ButtonManager(float delay)
    {
        yield return new WaitForSeconds(delay);
        onclick = false;
    }
}
