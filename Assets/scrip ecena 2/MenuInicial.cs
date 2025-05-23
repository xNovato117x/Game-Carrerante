using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene(1); // Asumiendo que la escena 1 es el juego
    }

    public void Opciones()
    {
        SceneManager.LoadScene(2); // Asumiendo que la escena 2 es tu pantalla de Opciones
    }

    public void Salir()
    {
        Debug.Log("Salir...");
        Application.Quit();
    }
}
