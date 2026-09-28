using UnityEngine;
using System.Collections;

public class RadiationZone : MonoBehaviour
{
    public RadiationSystemLevel systemLevel;
    public RadiationSystem RadiationSystem;

    public float radDamageTick = 5f;
    public float radDamageTimer;
    public float radDamage = 10f;

    public bool hasleft;




    public void Update()
    {
        if (hasleft == true)
        {
            StartCoroutine(CheckIfPlayerIsGone());
        }
        if (hasleft == false)
        {
            StopAllCoroutines();
        }
    }


    public void RadiationDamage()
    {
        if (RadiationSystem != null && systemLevel != null)
        {
            if (RadiationSystem.RadLevel.CurrentRadLevel >= systemLevel.CurrentRadLevel)
            {
                return;
            }
            if (RadiationSystem.RadLevel.CurrentRadLevel < systemLevel.CurrentRadLevel)
            {
                radDamageTimer += Time.deltaTime;
                if (radDamageTimer > radDamageTick)
                {
                    RadiationSystem.RadDamage(radDamage);
                    radDamageTimer = 0;
                }
            }
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            RadiationSystem radsystem = other.GetComponent<RadiationSystem>();
            hasleft = false;
            RadiationDamage();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hasleft = true;
        }
    }

    private IEnumerator CheckIfPlayerIsGone()
    {
        yield return new WaitForSeconds(3);
        if (hasleft == true)
        {
            radDamageTimer = 0;
            hasleft = false;
        }
    }
}
