using UnityEngine;

public class GlobalReference : MonoBehaviour
{
public static GlobalReference Instance { get; set; }

    public GameObject bulletHolePrefab;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}

       
