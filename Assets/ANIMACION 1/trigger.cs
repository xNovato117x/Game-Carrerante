using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] private playerController playerController;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Obstacle"))
        {
            playerController.HandleDeath();

            GameOver();

            //Invoke(nameof(GameOver), 2f);

        }
    }

    private void GameOver()
    {
        levelManager.LM.GameOver();
    }
}
