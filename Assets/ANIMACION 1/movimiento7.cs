using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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

    private Animator animator;

    [Header("Jump Control")]
    [SerializeField] float upwardMultiplier = 1.7f; 
    [SerializeField] float downwardMultiplier = 1.7f; 

    private float baseJumpForce; 

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    
    void Start()
    {
        destiny = transform.position;
        baseJumpForce = jumpForce;

        animator = GetComponent<Animator>();
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


        
        if ((Input.GetButtonDown("Up")) && (inFloor)) {
           
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
            inFloor = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            inFloor = false;
        }
    }
}