using UnityEngine;

public class TestFood : MonoBehaviour
{
    public TestDummyScript instance;

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<TestDummyScript>())
        {
            Debug.Log("Test Dummy");
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
        else
        {
            Debug.Log("Something is in here");
        }
    }
}
