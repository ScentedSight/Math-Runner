using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float barricadeSpawnRate;
    public float powerUpSpawnRate;
    public GameObject barricadePrefab;
    public GameObject powerUpPrefab;

    void Start()
    {
        PowerUp powerUp = new PowerUp();
    }

    void OnEnable()
    {
        
    }

    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
