using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSonido : MonoBehaviour
{
    // Volver al Men� Principal (escena 0)
    public void VolverAOpciones()
    {
        SceneManager.LoadScene(2);
    }
}

