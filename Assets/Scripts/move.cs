using System.Collections;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int health = 7;

    private Coroutine destructionCoroutine;

    void Start()
    {
        destructionCoroutine = StartCoroutine(Autodestruction());
    }

    void Update()
    {
        if (levelManager.isGameOver)
        {
            // Detener la autodestrucción si aún no se ha detenido
            if (destructionCoroutine != null)
            {
                StopCoroutine(destructionCoroutine);
                destructionCoroutine = null;
            }

            return;
        }

        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }

    IEnumerator Autodestruction()
    {
        yield return new WaitForSeconds(health);
        Destroy(gameObject);
    }
}

