using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [SerializeField] float timeToSpawn = 1.5f;
    public GameObject[] obsPref; // Obstáculos normales
    public GameObject specialObstaclePrefab; // Obstáculo especial

    private Vector3[] lanes = {
        new Vector3(10, 0, 0),
        new Vector3(20, 0, 0),
        new Vector3(30, 0, 0)
    };

    void OnEnable()
    {
        levelManager.OnRestart += StartSpawning;
        StartSpawning();
    }

    void OnDisable()
    {
        levelManager.OnRestart -= StartSpawning;
        CancelInvoke(nameof(Spawn));
    }

    void StartSpawning()
    {
        CancelInvoke(nameof(Spawn));
        InvokeRepeating(nameof(Spawn), 0f, timeToSpawn);
    }

    void Spawn()
    {
        if (levelManager.isGameOver) return;

        List<int> laneIndices = new List<int> { 0, 1, 2 };

        // Obstáculos normales en 2 carriles
        for (int i = 0; i < 2; i++)
        {
            int r = Random.Range(0, obsPref.Length);
            int laneIndex = laneIndices[Random.Range(0, laneIndices.Count)];
            laneIndices.Remove(laneIndex);

            Vector3 spawnPosition = lanes[laneIndex] + transform.position;
            Instantiate(obsPref[r], spawnPosition, Quaternion.identity);
        }

        // 40% de probabilidad para el especial
        if (Random.value <= 0.4f && laneIndices.Count > 0)
        {
            int specialLaneIndex = laneIndices[0];
            Vector3 specialSpawnPosition = lanes[specialLaneIndex] + transform.position;
            specialSpawnPosition.y -= 2f;
            Instantiate(specialObstaclePrefab, specialSpawnPosition, Quaternion.identity);
        }
    }
}
