using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision objectHit)
    {
        if (objectHit.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        
        if (objectHit.gameObject.CompareTag("Target"))
        {
            print("hit " + objectHit.gameObject.name + " !");
            CreateBulletImpactEffect(objectHit);
            Destroy(gameObject);
        }

        if (objectHit.gameObject.CompareTag("Wall"))
        {
            print("hit " + objectHit.gameObject.name + " !");
            CreateBulletImpactEffect(objectHit);
            Destroy(gameObject);
        }

        if (objectHit.gameObject.CompareTag("BeerBottle"))
        {
            print("hit " + objectHit.gameObject.name + " !");
            objectHit.gameObject.GetComponent<BeerBottlerr>().Shatter();        
        }

        if (objectHit.gameObject.CompareTag("Enemy"))
        {
            if (objectHit.gameObject.GetComponent<Health>() != null)
            {
                if (objectHit.gameObject.GetComponent<Health>().health > 0)
                {
                    objectHit.gameObject.GetComponent<Health>().onDamaged(5);
                }
                else if (objectHit.gameObject.GetComponent<Health>().health <= 0)
                {
                    Destroy(objectHit.gameObject);
                }
            }
        }
    }
    void CreateBulletImpactEffect(Collision objectHit)
    {
        ContactPoint contact = objectHit.contacts[0];

        GameObject hole = Instantiate(
            GlobalReference.Instance.bulletHolePrefab,
            contact.point,
            Quaternion.LookRotation(contact.normal));

        hole.transform.SetParent(objectHit.gameObject.transform);
    }
}
