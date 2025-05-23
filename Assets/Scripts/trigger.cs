using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            // Buscamos el componente playerController del jugador
            playerController player = coll.GetComponent<playerController>();

            if (player != null)
            {
                if (!player.isImmune) // Verificamos si NO está inmune
                {
                    player.HandleDeath();
                    levelManager.LM.GameOver();
                }
                else
                {
                    Debug.Log("Jugador inmune: no se activa GameOver.");
                }
            }
        }
    }
}
