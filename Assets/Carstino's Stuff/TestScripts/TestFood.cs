using UnityEngine;

public class TestFood : MonoBehaviour
{
    public TestDummyScript instance;

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<TestDummyScript>())
        {
            if (instance.hunger > 50)
            {
                return;
            }
            else
            {
                instance.hunger += 50;
                instance.foods.Remove(other.GetComponent<TestFood>());
                Destroy(gameObject);
            }
        }
    }
}
