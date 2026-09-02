using UnityEngine;

public class DealDamage : MonoBehaviour
{
    [SerializeField] private float damage;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Health enemy = other.GetComponent<Health>();
            enemy.TakeDamage(damage);
        }
    }
}
