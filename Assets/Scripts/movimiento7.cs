using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private Animator riderAnimator;

    [Header("Jump Control")]
    [SerializeField] float upwardMultiplier = 1.7f;
    [SerializeField] float downwardMultiplier = 1.7f;

    [Header("Player Audio")]
    [SerializeField] private AudioClip[] audioClips;

    private float baseJumpForce;
    private AudioManager audioManager;

    public int score = 0;
    public float scoreInterval = 1.0f;
    private float timer = 0.0f;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text coinText;

    // Inmunidad
    public bool isImmune = false;
    private float immunityDuration = 5f;

    // Monedas (solo por partida)
    private int coinsCollected = 0;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Start()
    {
        audioManager = AudioManager.Instance;
        destiny = transform.position;
        baseJumpForce = jumpForce;

        // ← Iniciar solo con 0 monedas para esta partida
        coinsCollected = 0;

        UpdateScoreText();
        UpdateCoinText();

        EnableHorseRun(true);

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
        if (levelManager.isGameOver) return;

        timer += Time.deltaTime;

        if (timer >= scoreInterval)
        {
            score += 2;
            UpdateScoreText();
            timer = 0;
        }

        if (!isMoving)
        {
            if (Input.GetButtonDown("Right")) TryMoveRight();
            if (Input.GetButtonDown("Left")) TryMoveLeft();
        }

        if (isMoving)
        {
            Vector3 xDestiny = new Vector3(destiny.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, xDestiny, moveSpeed * Time.deltaTime);

            if (Mathf.Abs(transform.position.x - destiny.x) <= movementTolerance)
            {
                transform.position = new Vector3(destiny.x, transform.position.y, transform.position.z);
                isMoving = false;
            }
        }

        if (Input.GetButtonDown("Up") && inFloor)
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            ActivateImmunity();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Coin"))
        {
            coinsCollected++;
            UpdateCoinText();

            // Guardar el total acumulado (no afecta lo que se ve en esta partida)
            int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
            PlayerPrefs.SetInt("TotalCoins", totalCoins + 1);
            PlayerPrefs.Save();

            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Obstacle"))
        {
            if (!isImmune)
            {
                levelManager.LM.GameOver();
                HandleDeath();
            }
        }
    }

    public void ActivateImmunity()
    {
        StartCoroutine(ImmunityRoutine());
    }

    IEnumerator ImmunityRoutine()
    {
        isImmune = true;
        yield return new WaitForSeconds(immunityDuration);
        isImmune = false;
    }

    private void EnableHorseRun(bool value)
    {
        if (value)
        {
            animator.SetBool("Run", true);
            audioManager.PlaySFX(audioClips[0], true);

            riderAnimator.enabled = false;
            riderAnimator.SetBool("Death", false);
            animator.SetBool("Death", false);
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
        riderAnimator.enabled = true;
        riderAnimator.SetBool("Death", true);
        animator.SetBool("Death", true);
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Puntuación: " + score;
        }
    }

    void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = "Monedas: " + coinsCollected;
        }
    }

    public int GetCoins()
    {
        return coinsCollected;
    }
}
