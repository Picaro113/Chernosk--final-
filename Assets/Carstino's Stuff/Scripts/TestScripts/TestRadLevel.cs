using UnityEngine;

public class TestRadLevel : MonoBehaviour
{
    public RadiationSystemLevel radiationSystem;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            RadiationSystemLevel.CurrentRadState CurrentState = RadiationSystemLevel.CurrentRadState.RadLevelTwo;
            radiationSystem.CurrentRadLevel += 1;
            radiationSystem.RadStatus = CurrentState;
            Destroy(gameObject);
        }
    }
}
