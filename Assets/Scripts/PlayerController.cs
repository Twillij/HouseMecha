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
    public KeyCode interactKey = KeyCode.Mouse0;

    private CharacterController characterController;
    private Animator animator;
    private Vector3 moveDirection;
    private float airVelocity;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
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
            if (airVelocity < 0.0f)
                airVelocity = -1.0f;

            // get the local move direction vector
            moveDirection = new Vector3(horizontal, 0.0f, vertical);
            
            if (moveDirection.magnitude > 0.0f)
            {
                //animator.SetBool("isWalking", true);
                Turn();
            }
            else
            {
                //animator.SetBool("isWalking", false);
            }

            // transform the local direction into world space
            moveDirection = transform.TransformDirection(moveDirection);

            if (isJumping)
            {
                // add an upward velocity relative to gravity
                airVelocity += Mathf.Sqrt(jumpPower * -3.0f * gravity);
            }
        }

        // move the player in the movement direction
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

        // apply gravity
        airVelocity += gravity * Time.deltaTime;

        // move the player in the vertical direction
        characterController.Move(new Vector3(0.0f, airVelocity, 0.0f) * Time.deltaTime);
    }

    private void Turn()
    {
        // set the player's y-rotation to be the same as the camera's
        Quaternion newRotation = Quaternion.Euler(this.transform.rotation.x, cameraController.transform.eulerAngles.y, this.transform.rotation.z);

        // execute the rotation smoothly
        this.transform.rotation = newRotation;// Quaternion.Slerp(this.transform.rotation, newRotation, turnSpeed * Time.deltaTime);
    }

    private void AttachItem(GameObject newItem)
    {
        AttachableSlot[] slots = transform.GetComponentsInChildren<AttachableSlot>();

        foreach (AttachableSlot slot in slots)
        {
            if (slot.CanAttach(newItem))
                slot.Attach(newItem);
        }
    }

    private void IncreaseScale(Vector3 addedScale)
    {
        transform.localScale += addedScale;
    }

    private void Update()
    {
        Move();

        if (Input.GetKeyDown(interactKey))
        {
            Ray ray = new Ray(transform.position + characterController.center, transform.forward);
            if (Physics.SphereCast(ray, characterController.radius, out RaycastHit hitInfo, characterController.radius))
            {
                AttachItem(hitInfo.transform.gameObject);
            }
        }
    }
}
