using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float health;

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log(health);
    }
}
