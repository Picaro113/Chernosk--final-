using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class TestFood : MonoBehaviour
{
    public void OnTriggerStay(Collider other)
    {
        Searchers searching = other.GetComponent<Searchers>();
        if (searching)
        {
            if (searching.hunger > 50)
            {
                return;
            }
            else
            {
                searching.hunger += 50;
                Destroy(gameObject);
            }
        }
    }
}
