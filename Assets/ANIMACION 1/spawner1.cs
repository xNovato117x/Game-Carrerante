using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner1 : MonoBehaviour
{
    [SerializeField] float timeToSpawn = 1.5f; // Tiempo entre apariciones
    public GameObject[] obsPref; // Prefabs de obstáculos

    // Definir solo la posición del carril central
    private Vector3 lane = new Vector3(20, 0, 0); // Carril 2
    private Queue<GameObject> inactiveObjects = new Queue<GameObject>(); // Cola para objetos inactivos

    void Start()
    {
        Spawn(); // Iniciar el proceso de spawn
    }

    void Spawn()
    {
        // Generar hasta 2 objetos en el carril central
        for (int i = 0; i < 2; i++)
        {
            GameObject objToSpawn = GetInactiveObject(); // Obtener un objeto inactivo
            Vector3 spawnPosition = lane + transform.position; // Posición de aparición

            if (objToSpawn != null)
            {
                // Reutilizar objeto inactivo
                objToSpawn.transform.position = spawnPosition; // Restablecer posición
                objToSpawn.SetActive(true); // Activar el objeto
            }
            else
            {
                // Instanciar nuevo objeto si no hay inactivos
                if (obsPref.Length > 0)
                {
                    int r = Random.Range(0, obsPref.Length);
                    GameObject newObj = Instantiate(obsPref[r], spawnPosition, Quaternion.identity);
                    inactiveObjects.Enqueue(newObj); // Guardar el nuevo objeto en la cola
                }
            }
        }

        // Invocar nuevamente el método Spawn después del tiempo definido
        Invoke("Spawn", timeToSpawn);
    }

    GameObject GetInactiveObject()
    {
        if (inactiveObjects.Count > 0)
        {
            return inactiveObjects.Dequeue(); // Obtener un objeto desactivado
        }
        return null; // No hay objetos desactivados disponibles
    }

    // Método para desactivar el objeto cuando sale del mapa
    public void DeactivateObject(GameObject obj)
    {
        obj.SetActive(false); // Desactivar el objeto
        inactiveObjects.Enqueue(obj); // Agregar a la cola de objetos inactivos
    }
}