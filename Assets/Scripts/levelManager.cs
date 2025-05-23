using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelManager : MonoBehaviour
{
    public static levelManager LM;
    public static bool isGameOver = false;

    // 🔧 AÑADIMOS ESTA LÍNEA:
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
        print("GameOver");

        isGameOver = true;
        mGameOver.SetActive(true);
        AudioManager.Instance.StopMusic();

        CancelInvoke();
    }

    public void Restart()
    {
        isGameOver = false;
        mGameOver.SetActive(false);

        // 🔧 INVOCA EL EVENTO PARA REACTIVAR LOS SPAWNERS
        OnRestart?.Invoke();

        SceneManager.LoadScene(1);
    }

    public void BackToMenu()
    {
        isGameOver = false;
        mGameOver.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
