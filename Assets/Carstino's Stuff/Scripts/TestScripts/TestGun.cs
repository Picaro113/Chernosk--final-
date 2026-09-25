using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TestGun : MonoBehaviour
{
    public GameObject bullet;
    public Transform gun;

    public void Update()
    {
        Instantiate(bullet, gun.position, gun.rotation);
    }
}
