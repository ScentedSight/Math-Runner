using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float barricadeSpawnRate;
    public float powerUpSpawnRate;
    public GameObject barricadePrefab;
    public GameObject powerUpPrefab;

    private void SpawnBarricade()
    {
        int randomIndex = Random.Range(0, 2);
        if (randomIndex == 0)
        {
            Instantiate(barricadePrefab, new Vector3(RoadLoop.leftLaneX + 2, transform.position.y, transform.position.z), barricadePrefab.transform.rotation);
        }
        else if (randomIndex == 1)
        {
            Instantiate(barricadePrefab, new Vector3(RoadLoop.rightLaneX + 2, transform.position.y, transform.position.z), barricadePrefab.transform.rotation);
        }
    }

    private IEnumerator SpawnBarricades()
    {
        while (true)
        {
            SpawnBarricade();
            yield return new WaitForSeconds(barricadeSpawnRate);
        }
    }

    public void SetBarricadeSpawnRate(int currentLevel)
    {
        if (currentLevel <= 10)
        {
            barricadeSpawnRate = 5f;
        }
        else if (currentLevel <= 20)
        {
            barricadeSpawnRate = 4f;
        }
        else if (currentLevel <= 30)
        {
            barricadeSpawnRate = 3f;
        }
        else
        {
            barricadeSpawnRate = 2f;
        }
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnBarricades());
    }
}
