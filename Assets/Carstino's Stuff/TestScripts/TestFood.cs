using UnityEngine;

public class TestFood : MonoBehaviour
{
    public Searchers instance;

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Searchers>())
        {
            if (instance.hunger > 50)
            {
                return;
            }
            else
            {
                instance.hunger += 50;
                Destroy(gameObject);
            }
        }
    }
}
