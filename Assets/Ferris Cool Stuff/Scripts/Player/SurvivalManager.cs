using UnityEngine;

public class SurvivalManager : MonoBehaviour
{
    [Header("Hunger")]
    [SerializeField] private float maxHunger;
    [SerializeField] private float hungerDrainRate = 1f;
    private float currentHunger;
    public float hungerPercent => currentHunger / maxHunger;

    [Header("Thirst")]
    [SerializeField] private float maxThirst;
    [SerializeField] private float thirstDrainRate = 1f;
    private float currentThirst;
    public float thirstPercent => currentThirst / maxThirst;
}
