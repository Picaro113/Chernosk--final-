using UnityEngine;

public class TestFood : MonoBehaviour
{
    public Searchers instance;

    public void Start()
    {
        
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent<Searchers>(out Searchers searchers))
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
