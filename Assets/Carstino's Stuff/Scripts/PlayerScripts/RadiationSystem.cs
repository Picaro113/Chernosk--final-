using System.Linq;
using UnityEngine;

public class RadiationSystem : MonoBehaviour
{
    public RadiationSystemLevel RadLevel;
    public float RadiationHealth = 100f;


    public void Start()
    {
        Convert();
    }

    public void Update()
    {
        Debug.Log(RadiationHealth);
    }

    public void Convert()
    {
        RadLevel.CurrentRadLevel += 1;
        RadiationSystemLevel.CurrentRadState CurrentState = RadiationSystemLevel.CurrentRadState.RadLevelOne;
        RadLevel.CurrentRadLevel = (int)CurrentState + 1;
        RadLevel.RadStatus = CurrentState;
    }

    public void RadDamage(float damage)
    {
        RadiationHealth -= damage;
    }
}
