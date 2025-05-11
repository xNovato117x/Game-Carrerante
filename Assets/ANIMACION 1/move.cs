using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int health = 5;

    // Use this for initialization
    void Start()
    {
        Invoke("Autodestruction", health);
    }

    // Update is called once per frame
    void Update()
    {
        // Mover hacia atr�s en el eje Z
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }

    void Autodestruction()
    {
        Destroy(gameObject);
    }
}
