using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over");
        }
    }
}
