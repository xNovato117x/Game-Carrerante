using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelManager : MonoBehaviour
{
    public static levelManager LM;
    public static bool isGameOver = false;

    public static event Action OnRestart;

    [SerializeField] GameObject mGameOver;

    void Start()
    {
        LM = this;
        isGameOver = false;
        mGameOver.SetActive(false);
    }

    public void GameOver()
    {
        Debug.Log("GameOver");

        isGameOver = true;
        mGameOver.SetActive(true);

        // Detener música actual del juego
        var music = FindFirstObjectByType<MusicManager>();
        if (music != null)
        {
            music.StopMusic();
        }

        CancelInvoke();
    }

    public void Restart()
    {
        isGameOver = false;
        mGameOver.SetActive(false);

        OnRestart?.Invoke();

        // La música se reiniciará automáticamente al recargar la escena
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu()
    {
        isGameOver = false;
        mGameOver.SetActive(false);
        SceneManager.LoadScene(0); // Escena del menú
    }
}
