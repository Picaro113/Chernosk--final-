using UnityEngine;

public class Create : MonoBehaviour
{
    public GameObject growth;

    public enum Infected
    {
        True,
        False
    }

    public Infected infected;

    public void Start()
    {
        infected = (Infected)Random.Range(0, System.Enum.GetValues(typeof(Infected)).Length);
        Debug.Log(infected);
    }


    public void OnTriggerEnter(Collider other)
    {
        if (infected == Infected.True)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Instantiate(growth, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
        if (infected == Infected.False)
        {
            Destroy(gameObject);
        }
    }
}
