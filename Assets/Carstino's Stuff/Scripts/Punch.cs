using UnityEngine;

public class Punch : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private Collider punch;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            anim.SetTrigger("Attack");
        }
    }

    public void EnableWeaponCollider()
    {
        punch.enabled = true;

        Debug.Log("collider is on");
    }

    public void DisableWeaponCollider()
    {
        punch.enabled = false;

        Debug.Log("collider is off");
    }
}
