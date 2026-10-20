using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    public bool hover;

    public Color[] colors;
    public float duration;
    public GameObject sphere;
    
    private Renderer rend;
    private int currentColorIndex = 0;
    private float t = 0f;
    
    private void Start()
    {
        //rend = sphere.GetComponent<Renderer>();
        //hover = false;
    }
    
    private void Update()
    {
        AddColor();
        //if (hover == true)
        //{
        //    t += Time.deltaTime / duration;
        //    rend.material.color = Color.Lerp(colors[currentColorIndex], colors[(currentColorIndex + 1) % colors.Length], t);
        //
        //    if (t > 1f)
        //    {
        //        t = 0f;
        //        currentColorIndex = currentColorIndex + 1 % colors.Length;
        //    }
        //}
        //else if (hover == false && colors != null) 
        //{
        //    t += Time.deltaTime / duration;
        //    rend.material.color = Color.Lerp(colors[currentColorIndex], colors[(currentColorIndex - 1) % colors.Length], t);
        //
        //    if (t > 1f)
        //    {
        //        t = 0f;
        //        currentColorIndex = currentColorIndex - 1 % colors.Length;
        //    }
        //}
    }
    public void AddColor()
    {
        for (int i = 0; i < currentColorIndex; i++)
        {
            currentColorIndex += 1;
            Debug.Log(currentColorIndex);
        }
    }
    


    public void changeColor()
    {
        rend.material.color = Color.Lerp(colors[currentColorIndex], colors[(currentColorIndex + 1) % colors.Length], t);
        currentColorIndex = currentColorIndex + 1 % colors.Length;
    }
}
