using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    [SerializeField] float timeToSpawn = 1.5f;
    public List<GameObject> obsPref;

    // Use this for initialization
    void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        int r = Random.Range(0, obsPref.Count);
        Instantiate(obsPref[r], transform.position, transform.rotation);
        Invoke("Spawn", timeToSpawn);
    }
}