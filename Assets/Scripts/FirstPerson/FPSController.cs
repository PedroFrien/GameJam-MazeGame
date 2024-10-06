using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    public Camera playerCamera;

    [Header("Movement")]
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;

    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    [SerializeField] private float health;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;

    public bool slowed;

    [SerializeField] private Slider healthBarSlider;
    
    CharacterController characterController;


    // Start is called before the first frame update

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        healthBarSlider.maxValue = health;
        healthBarSlider.value = health;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        //Movement
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectoryY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        //Movement



        //Jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectoryY;
        }

        if(!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        //Jumping



        //Rotation
        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);

        }

        if (curSpeedX > 0 && !isRunning)
        {
            FindObjectOfType<AudioManager>().PlaySound("PlayerWalking", transform.position, gameObject);
        }
        if (isRunning)
        {
            FindObjectOfType<AudioManager>().PlaySound("PlayerRunning", transform.position, gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        healthBarSlider.value = health;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        healthBarSlider.gameObject.SetActive(false);
        FindObjectOfType<GameManager>().Die(false);
    }

}
