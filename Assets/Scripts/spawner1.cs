using System.Collections.Generic;
using UnityEngine;

public class Spawner1 : MonoBehaviour
{
    [SerializeField] float timeToSpawn = 1.5f;
    public GameObject[] obsPref;

    private Vector3 lane = new Vector3(20, 0, 0);
    private Queue<GameObject> inactiveObjects = new Queue<GameObject>();

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

        for (int i = 0; i < 2; i++)
        {
            GameObject objToSpawn = GetInactiveObject();
            Vector3 spawnPosition = lane + transform.position;

            if (objToSpawn != null)
            {
                objToSpawn.transform.position = spawnPosition;
                objToSpawn.SetActive(true);
            }
            else if (obsPref.Length > 0)
            {
                int r = Random.Range(0, obsPref.Length);
                GameObject newObj = Instantiate(obsPref[r], spawnPosition, Quaternion.identity);
                inactiveObjects.Enqueue(newObj);
            }
        }
    }

    GameObject GetInactiveObject()
    {
        if (inactiveObjects.Count > 0)
        {
            return inactiveObjects.Dequeue();
        }
        return null;
    }

    public void DeactivateObject(GameObject obj)
    {
        obj.SetActive(false);
        inactiveObjects.Enqueue(obj);
    }
}
