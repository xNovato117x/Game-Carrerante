using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMapa : MonoBehaviour
{
    // Volver al Menú Principal (escena 0)
    public void VolverAOpci()
    {
        SceneManager.LoadScene(2);
    }
}

