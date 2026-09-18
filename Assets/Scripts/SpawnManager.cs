using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float barricadeSpawnRate = 5f;
    [SerializeField] private float powerUpSpawnRate = 30f;
    public GameObject barricadePrefab;
    public GameObject powerUpPrefab;

    private void SpawnBarricade()
    {
        int randomIndex = Random.Range(0, 2);
        float spawnX;
        //Randomly spawn barricade either left or right
        if (randomIndex == 0)
        {
            spawnX = RoadLoop.leftLaneX + 2;
        }
        else
        {
            spawnX = RoadLoop.rightLaneX + 2;
        }

        Vector3 spawnPosition = new Vector3(spawnX, transform.position.y, transform.position.z);

        // Check if something is already at this location
        if (!Physics.CheckBox(spawnPosition,new Vector3(1f, 1f, 5f)))
        {
            Instantiate(barricadePrefab, spawnPosition, barricadePrefab.transform.rotation);
        }
    }

    private IEnumerator SpawnBarricades()
    {
        //Spawn barricade as a separate couroutine process while taking in spawn rate as the wait length parameter
        while (true)
        {
            SpawnBarricade();
            yield return new WaitForSeconds(barricadeSpawnRate);
        }
    }

    private void SpawnPowerUp()
    {
        int randomIndex = Random.Range(0, 2);

        float spawnX;

        if (randomIndex == 0)
        {
            spawnX = RoadLoop.leftLaneX;
        }
        else
        {
            spawnX = RoadLoop.rightLaneX;
        }

        Vector3 spawnPosition = new Vector3(spawnX, powerUpPrefab.transform.position.y, powerUpPrefab.transform.position.z);

        // Check if something is already at this location
        if (!Physics.CheckBox(spawnPosition,new Vector3(1f, 1f, 5f)))
        {
            Instantiate(powerUpPrefab, spawnPosition, powerUpPrefab.transform.rotation);
        }
    }

    private IEnumerator SpawnPowerUps()
    {
        //Spawn power ups as a separate couroutine process while taking in spawn rate as the wait length parameter
        while (true)
        {
            SpawnPowerUp();
            yield return new WaitForSeconds(powerUpSpawnRate);
        }
    }

    public void SetSpawnRate(int currentLevel)
    {
        //Setting random spawn rate based on level difficulty
        if (currentLevel <= 10)
        {
            barricadeSpawnRate = Random.Range(4f, 5f);
            powerUpSpawnRate = Random.Range(25f, 30f);
        }
        else if (currentLevel <= 20)
        {
            barricadeSpawnRate = Random.Range(3f, 4f);
            powerUpSpawnRate = Random.Range(20f, 25f);
        }
        else if (currentLevel <= 30)
        {
            barricadeSpawnRate = Random.Range(2f, 3f);
            powerUpSpawnRate = Random.Range(15f, 20f);
        }
        else
        {
            barricadeSpawnRate = Random.Range(1f, 2f);
            powerUpSpawnRate = Random.Range(10f, 15f);
        }
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnBarricades());
        StartCoroutine(SpawnPowerUps());
    }
}
