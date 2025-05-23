using UnityEngine;
using UnityEngine.SceneManagement;

public class OpcionesMenu : MonoBehaviour
{
    // Volver al Menú Principal (escena 0)
    public void VolverAlMenu()
    {
        SceneManager.LoadScene(0);
    }

    // Ir a la escena de Sonidos (escena 3)
    public void IrASonidos()
    {
        SceneManager.LoadScene(3);
    }

    // Ir a la escena de Interfaz de Mapas (escena 4)
    public void IrAInterfazMapas()
    {
        SceneManager.LoadScene(4);
    }

    // Ir a la escena de Cuenta (escena 5)
    public void IrACuenta()
    {
        SceneManager.LoadScene(5);
    }

    // Ir a la escena de Créditos (escena 6)
    public void IrACreditos()
    {
        SceneManager.LoadScene(6);
    }
}
