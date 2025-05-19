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
        // Aumentar el temporizador
        timer += Time.deltaTime;

        // Comprobar si ha pasado el intervalo
        if (timer >= scoreInterval)
        {
            score += 2;                  // Sumar 2 puntos
            UpdateScoreText();           // Actualizar el texto de la puntuación
            timer = 0;                   // Reiniciar el temporizador
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