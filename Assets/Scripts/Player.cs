using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{

    public float baseSpeed;
    public float runningAmplifier;

    public bool lockCursor;

    private CharacterController characterController;
    private Animator animator;

    private float airTime;
    private float gravity = -9.81f;

    private PlayerMovementInfo playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        playerMovement = new PlayerMovementInfo();

        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        airTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();

        PerformBlendTreeAnimation();

        CalculateDirectionAndDistance();
        PerformPhysicalMovement();

        RotatePlayerWithCamera();
    }

    public void ProcessInput()
    {
        playerMovement.leftAndRight = Input.GetAxis("Horizontal"); //A and D
        playerMovement.forwardAndBackward = Input.GetAxis("Vertical"); //W and S, -1 to 1

        playerMovement.movingForwards = playerMovement.forwardAndBackward > 0.0f;
        playerMovement.movingBackwards = playerMovement.forwardAndBackward < 0.0f;

        bool running = (playerMovement.movingForwards && Input.GetKey(KeyCode.LeftControl))
                        ||
                        (!playerMovement.movingBackwards && (playerMovement.leftAndRight > 0.0f || playerMovement.leftAndRight < 0.0f));

        if (running)
        {
            playerMovement.speed = playerMovement.baseSpeed * playerMovement.runningAmplifier;
        }
        else
        {
            playerMovement.speed = playerMovement.baseSpeed;
            playerMovement.forwardAndBackward = playerMovement.forwardAndBackward / 2.0f;
        }
    }
    public void PerformBlendTreeAnimation()
    {
        float leftAndRight = playerMovement.leftAndRight;

        if (playerMovement.movingBackwards)
        {
            leftAndRight = 0.0f;
        }

        animator.SetFloat("leftAndRight", leftAndRight);
        animator.SetFloat("forwardAndBackward", playerMovement.forwardAndBackward);

    }

    public void CalculateDirectionAndDistance()
    {
        //Uses player transform to calculate direction to move.
        Vector3 moveDirectionForward = transform.forward * playerMovement.forwardAndBackward;
        Vector3 moveDirectionSide = transform.right * playerMovement.leftAndRight;

        //Get direction the player is moving in
        playerMovement.direction = moveDirectionForward + moveDirectionSide;
        playerMovement.normalizedDirection = playerMovement.direction.normalized;

        //Get speed to move at
        playerMovement.distance = playerMovement.normalizedDirection * playerMovement.speed * Time.deltaTime;

        GroundPlayer();
    }

    public void PerformPhysicalMovement()
    {
        characterController.Move(playerMovement.distance);
    }

    public void GroundPlayer()
    {
        //If on the ground
        if (characterController.isGrounded)
        {
            airTime = 0;
        }
        //If in the air
        else
        {
            airTime += Time.deltaTime;
            Vector3 direction = playerMovement.normalizedDirection;

            direction.y += 0.5f * gravity * airTime;

            playerMovement.normalizedDirection = direction;
            playerMovement.distance = playerMovement.normalizedDirection * airTime;
        }
    }

    public void RotatePlayerWithCamera()
    {

        if (Input.GetKey(KeyCode.T))
        {
            return; //makes camera orbit player.
        }
        else
        {
            Vector3 rotation;

            rotation = Camera.main.transform.eulerAngles;
            rotation.x = 0;
            rotation.z = 0;

            transform.eulerAngles = rotation;
        }
    }
}
