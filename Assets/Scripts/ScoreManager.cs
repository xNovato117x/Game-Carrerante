using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;               // Puntuación inicial
    public float scoreInterval = 1.0f;  // Intervalo de tiempo para sumar puntos
    private float timer = 0.0f;         // Temporizador

    public Text scoreText;              // Referencia al UI de texto para mostrar la puntuación

    void Start()
    {
        // Inicializa la puntuación en el UI
        UpdateScoreText();
    }
    void Update()
    {
        if (levelManager.isGameOver) return;

        timer += Time.deltaTime;

        if (timer >= scoreInterval)
        {
            score += 2;
            UpdateScoreText();
            timer = 0;
        }
    }


    void UpdateScoreText()
    {
        // Actualiza el texto de la puntuación en el UI
        if (scoreText != null)
        {
            scoreText.text = "Puntuación: " + score;
        }
    }
}