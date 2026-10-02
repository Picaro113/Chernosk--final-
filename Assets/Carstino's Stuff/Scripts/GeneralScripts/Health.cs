using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;

    public float maxhealth;

    public void Start()
    {
        health = maxhealth;
    }

    public void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void onDamaged(float damage)
    {
        health -= damage;
    }

    public void onHeal(float heal)
    {
        health += heal;
    }
}
