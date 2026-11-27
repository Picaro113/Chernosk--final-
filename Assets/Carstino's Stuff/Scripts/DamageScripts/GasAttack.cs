using UnityEngine;

public class GasAttack : MonoBehaviour
{
    float tickdamage = 0;

    private void Start()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(5, 5, 5), .1f * Time.deltaTime);
    }

    public void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            tickdamage += Time.deltaTime;
            if (tickdamage > 1)
            {
                other.GetComponent<Health>().onDamaged(5);
                tickdamage = 0;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            tickdamage = 0;
        }
    }
}
