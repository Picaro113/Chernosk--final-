using UnityEngine;
using UnityEngine.Events;

public class SurvivalManager : MonoBehaviour
{
    [Header("Hunger")]
    [SerializeField] private float maxHunger = 100f;
    [SerializeField] private float hungerDrainRate = 1f;
    [SerializeField] private float currentHunger;
    public float hungerPercent => currentHunger / maxHunger;

    [Header("Thirst")]
    [SerializeField] private float maxThirst = 100f;
    [SerializeField] private float thirstDrainRate = 1f;
    [SerializeField] private float currentThirst;
    public float thirstPercent => currentThirst / maxThirst;

    [Header("Stamina")]
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float staminaDrainRate = 1f;
    [SerializeField] private float staminaRecoveryRate = 2f;
    [SerializeField] private float staminaRecoveryDelay = 1f;
    private float currentStamina;
    private float currentStaminaRecoveryDelay;
    public float staminaPercent => currentStamina / maxStamina;

    [Header("Player References")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Health playerHealth;
    [SerializeField] private float damagePerSecond = 0.5f;
    [SerializeField] private float damageDelay = 3f;
    [SerializeField] private float damageTick;

    public static UnityAction OnPlayerDeath;

    private void Start()
    {
        currentHunger = maxHunger;
        currentThirst = maxThirst;
        currentStamina = maxStamina;
    }

    private void Update()
    {
        currentHunger -= hungerDrainRate * Time.deltaTime;
        currentThirst -= thirstDrainRate * Time.deltaTime;
        damageTick += Time.deltaTime;

        if (currentHunger <= 0 || currentThirst <= 0)
        {
            if (damageTick >= damageDelay)
            {
                playerHealth.onDamaged(damagePerSecond);
                currentHunger = 0;
                currentThirst = 0;
                damageTick = 0f;
            }
        }
        if (playerMovement.isRunning)
        {
            currentStamina -= staminaDrainRate * Time.deltaTime;
            currentStaminaRecoveryDelay = 0f;
        }
        if (!playerMovement.isRunning && currentStamina < maxStamina)
        {          
            if (currentStaminaRecoveryDelay < staminaRecoveryDelay)
            {
                currentStamina += Time.deltaTime;
            }
            if(currentStaminaRecoveryDelay >= staminaRecoveryDelay)
            {
                currentStamina += staminaRecoveryRate * Time.deltaTime;
                if(currentStamina > maxStamina)
                {
                    currentStamina = maxStamina;
                }
            }
        }
    }
}
    

