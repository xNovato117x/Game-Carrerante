using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject itemPrefab;
    [SerializeField] float timeToSpawn = 2f;
    [SerializeField] float itemYOffset = -0.5f;
    [SerializeField, Range(0f, 1f)] float itemSpawnChance = 0.6f;

    private Vector3[] lanes = {
        new Vector3(10, 0, 0),
        new Vector3(20, 0, 0),
        new Vector3(30, 0, 0)
    };

    void Start()
    {
        Invoke("SpawnItem", timeToSpawn);
    }

    void SpawnItem()
    {
        if (levelManager.isGameOver)
            return;

        if (Random.value < itemSpawnChance)
        {
            // Elegir un carril aleatorio
            int laneIndex = Random.Range(0, lanes.Length);
            Vector3 spawnPos = lanes[laneIndex] + transform.position + new Vector3(0, itemYOffset, 0);
            Instantiate(itemPrefab, spawnPos, Quaternion.identity);
        }

        // Invocar la siguiente aparición
        Invoke("SpawnItem", timeToSpawn);
    }
}
