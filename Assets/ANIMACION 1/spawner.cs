using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] float timeToSpawn = 1.5f;
    public GameObject[] obsPref;

    // Definir posiciones para los carriles
    private Vector3[] lanes = {
        new Vector3(10, 0, 0), // Carril 1
        new Vector3(20, 0, 0), // Carril 2
        new Vector3(30, 0, 0)  // Carril 3
    };

    void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        // Generar hasta 2 objetos
        for (int i = 0; i < 2; i++)
        {
            int r = Random.Range(0, obsPref.Length);
            int laneIndex = Random.Range(0, lanes.Length);
            Vector3 spawnPosition = lanes[laneIndex] + transform.position; // Asegúrate de que sea delante del spawner

            Instantiate(obsPref[r], spawnPosition, Quaternion.identity);
        }

        Invoke("Spawn", timeToSpawn);
    }
}