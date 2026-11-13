using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletDamage : MonoBehaviour
{
    Scene scene;

    public void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<Health>() != null)
            {
                if (other.GetComponent<Health>().health > 0)
                {
                    other.GetComponent<Health>().onDamaged(10);
                }
                else if (other.GetComponent<Health>().health <= 0)
                {
                    SceneManager.LoadScene(scene.name, LoadSceneMode.Single);
                }
            }
            //Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, 5);
        }
    }
}
