using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI; // Asegúrate de incluir esto si usas UI

public class playerController : MonoBehaviour
{
    Rigidbody rigid;

    [Range(-1, 1)][SerializeField] int position;
    Vector3 destiny;
    bool inFloor;

    [Header("SETUP")]
    [SerializeField] float moveSpeed = 1;
    [SerializeField] float jumpForce = 1;

    [SerializeField] float horizontalDistance = 1f;
    [SerializeField] float jumpHeightMultiplier = 1f;

    private bool isMoving = false;
    private float movementTolerance = 0.05f;

    [SerializeField] private Animator animator;

    [Header("Jump Control")]
    [SerializeField] float upwardMultiplier = 1.7f;
    [SerializeField] float downwardMultiplier = 1.7f;

    [Header("Player Audio")]
    [SerializeField] private AudioClip[] audioClips;

    private float baseJumpForce;
    private AudioManager audioManager;

    // Variables para el sistema de puntuación
    public int score = 0;               // Puntuación inicial
    public float scoreInterval = 1.0f;  // Intervalo para sumar puntos
    private float timer = 0.0f;         // Temporizador

    [SerializeField] private Text scoreText; // Referencia al texto de UI para mostrar la puntuación

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Start()
    {
        audioManager = AudioManager.Instance;
        destiny = transform.position;
        baseJumpForce = jumpForce;

        // Inicializa la puntuación en el UI
        UpdateScoreText();
    }

    void FixedUpdate()
    {
        if (rigid.linearVelocity.y > 0)
        {
            rigid.linearVelocity += Vector3.up * Physics.gravity.y * (upwardMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rigid.linearVelocity.y < 0)
        {
            rigid.linearVelocity += Vector3.up * Physics.gravity.y * (downwardMultiplier - 1) * Time.fixedDeltaTime;
        }
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

        if (!isMoving)
        {
            if (Input.GetButtonDown("Right"))
            {
                TryMoveRight();
            }
            if (Input.GetButtonDown("Left"))
            {
                TryMoveLeft();
            }
        }

        if (isMoving)
        {
            Vector3 xDestiny = new Vector3(destiny.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, xDestiny, moveSpeed * Time.deltaTime);

            if (Mathf.Abs(transform.position.x - destiny.x) <= movementTolerance)
            {
                transform.position = new Vector3(destiny.x, transform.position.y, transform.position.z); // Ensure exact position
                isMoving = false;
            }
        }

        if ((Input.GetButtonDown("Up")) && (inFloor))
        {
            jumpForce = baseJumpForce * upwardMultiplier;
            rigid.AddForce(Vector3.up * jumpForce * jumpHeightMultiplier, ForceMode.Impulse);
            jumpForce = baseJumpForce;
            animator.SetTrigger("Jump");
        }
    }

    void TryMoveRight()
    {
        if (position < 1)
        {
            destiny.x = transform.position.x + horizontalDistance;
            position++;
            isMoving = true;
        }
    }

    void TryMoveLeft()
    {
        if (position > -1)
        {
            destiny.x = transform.position.x - horizontalDistance;
            position--;
            isMoving = true;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (!inFloor)
            {
                EnableHorseRun(true);
                inFloor = true;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (inFloor)
            {
                EnableHorseRun(false);
                inFloor = false;
            }
        }
    }

    private void EnableHorseRun(bool value)
    {
        if (value)
        {
            animator.SetBool("Run", true);
            audioManager.PlaySFX(audioClips[0], true);
        }
        else
        {
            animator.SetBool("Run", false);
            audioManager.StopSFX(audioClips[0]);
        }
    }

    public void HandleDeath()
    {
        EnableHorseRun(false);
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Puntuación: " + score;
        }
    }
}