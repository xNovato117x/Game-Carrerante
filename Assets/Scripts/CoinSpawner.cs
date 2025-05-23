using UnityEngine;

public class CoinLineSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public float spawnInterval = 1f;
    public int coinsPerLine = 4;
    public float coinSpacing = 1f;

    private Vector3[] lanes = {
        new Vector3(10, 1f, 0), // Carril izquierdo
        new Vector3(20, 1f, 0), // Carril central
        new Vector3(30, 1f, 0)  // Carril derecho
    };

    void Start()
    {
        InvokeRepeating(nameof(SpawnCoins), 0f, spawnInterval);
    }

    void SpawnCoins()
    {
        if (levelManager.isGameOver) return;

        int randomLane = Random.Range(0, lanes.Length);
        Vector3 startPosition = lanes[randomLane] + transform.position;

        for (int i = 0; i < coinsPerLine; i++)
        {
            Vector3 spawnPos = startPosition + new Vector3(0, 0, i * coinSpacing);
            Instantiate(coinPrefab, spawnPos, Quaternion.identity);
        }
    }
}
