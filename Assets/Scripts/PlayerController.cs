using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    public CameraController cameraController;
    public float moveSpeed = 5;
    public float turnSpeed = 5;
    public float jumpPower = 1;
    public float gravity = -9.81f;
    public KeyCode jumpKey = KeyCode.Space;

    private CharacterController characterController;
    
    private Vector3 moveDirection;
    private float verticalVelocity;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Move()
    {
        if (characterController.isGrounded)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            bool isJumping = Input.GetKeyDown(jumpKey);

            // reset the vertical velocity to a low negative number
            // this is to ensure that the player is completely grounded
            // if set to 0 then the player might hover slightly in the air, i.e. not grounded
            if (verticalVelocity < 0.0f)
                verticalVelocity = -1.0f;

            // get the local move direction vector
            moveDirection = new Vector3(horizontal, 0.0f, vertical);
            
            if (moveDirection.magnitude > 0.0f)
            {
                Turn();
            }

            // transform the local direction into world space
            moveDirection = transform.TransformDirection(moveDirection);

            if (isJumping)
            {
                // add an upward velocity relative to gravity
                verticalVelocity += Mathf.Sqrt(jumpPower * -3.0f * gravity);
            }
        }

        // move the player in the movement direction
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

        // apply gravity
        verticalVelocity += gravity * Time.deltaTime;

        // move the player in the vertical direction
        characterController.Move(new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime);
    }

    private void Turn()
    {
        // set the player's y-rotation to be the same as the camera's
        Quaternion newRotation = Quaternion.Euler(this.transform.rotation.x, cameraController.transform.eulerAngles.y, this.transform.rotation.z);

        // execute the rotation smoothly
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, newRotation, turnSpeed * Time.deltaTime);
    }

    private void Update()
    {
        Move();
    }
}
