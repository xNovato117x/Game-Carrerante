using UnityEngine;

public class PowerUpItem : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController controller = other.GetComponent<playerController>();
            if (controller != null)
            {
                controller.ActivateImmunity(); // Llama al método directamente
            }

            Destroy(gameObject);
        }
    }
}


