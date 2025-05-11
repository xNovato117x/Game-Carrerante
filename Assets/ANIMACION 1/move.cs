using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int health = 5;

    void Start()
    {
        StartCoroutine(Autodestruction());
    }

    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }

    IEnumerator Autodestruction()
    {
        // Esperar el tiempo definido antes de destruir
        yield return new WaitForSeconds(health);
        Destroy(gameObject);
    }
}