using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BeerBottlerr : MonoBehaviour
{
  public List<Rigidbody> allParts = new List<Rigidbody>();

    public void Shatter()
    {
        foreach(Rigidbody part in allParts)
        {
            part.isKinematic = false;
            gameObject.GetComponent<Collider>().enabled = false;       
        }
    }
}
