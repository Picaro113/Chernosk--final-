using UnityEngine;

public class TestBullet : MonoBehaviour
{
    public void Update()
    {
        transform.position += transform.forward * 10 * Time.deltaTime; 
    }


    public void OnTriggerEnter(Collider other)
    {
        Searchers searching = other.GetComponent<Searchers>();

        if (searching && other.gameObject == gameObject)
        {
            Debug.Log("Something Else");
            Destroy(gameObject, 5f);
        }
        if (searching && other.gameObject != gameObject)
        {
            Debug.Log("Hit enemy");
            Destroy(gameObject);
        }
    }
}
