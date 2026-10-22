using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class ChangeColor : MonoBehaviour
{
    public bool hover;

    public Color[] colors;
    public float duration;
    public GameObject sphere;
    
    private Renderer rend;
    private int currentColorIndex = 0;
    private float t = 0f;

    public bool onclick;
    public float timer;

    public List<Button> buttons = new List<Button>();
    
    private void Start()
    {
        rend = sphere.GetComponent<Renderer>();
        hover = false;
        currentColorIndex = 0;
        rend.material.color = colors[currentColorIndex];

    }
    
    private void Update()
    {

        //t += Time.deltaTime / duration;
        //rend.material.color = Color.Lerp(colors[currentColorIndex], colors[(currentColorIndex + 1) % colors.Length], t);
        //
        //if (t > 1f)
        //{
        //    t = 0f;
        //    currentColorIndex = currentColorIndex + 1 % colors.Length;
        //}
        //
        //t += Time.deltaTime / duration;
        //
        //if (t > 1f)
        //{
        //    t = 0f;
        //    
        //}

        if (onclick == true)
        {
            StartCoroutine(ButtonManager(timer));
        }
        if (onclick == false)
        {
            StopAllCoroutines();
        }
    }

    public void changeColorAdd()
    {
        //rend.material.color = Color.Lerp(colors[currentColorIndex], colors[(currentColorIndex + 1)], t);
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
        //rend.material.color = Color.Lerp(colors[currentColorIndex], colors[(currentColorIndex - 1)], t);

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
