using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]

public class CameraController : MonoBehaviour
{
    public float sensitivityX = 1000;
    public float sensitivityY = 500;
    public float minAngleY = -60;
    public float maxAngleY = 60;

    public Transform target;
    public Vector3 orbitRadius;

    private float angleX = 0;
    private float angleY = 0;

    private void Start()
    {
        orbitRadius = transform.position - target.position;
        Cursor.lockState = CursorLockMode.Locked;

        // if there is no target, try to set the player as target
        if (target == null)
        {
            PlayerController playerController = FindObjectOfType<PlayerController>();

            if (playerController != null)
            {
                target = playerController.transform;
            }
            else
            {
                Debug.LogError("No Player Controller found.");
            }
        }
    }
    
    private void Update()
    {
        angleX += Input.GetAxis("Mouse X") * sensitivityX * Time.deltaTime;
        angleY -= Input.GetAxis("Mouse Y") * sensitivityY * Time.deltaTime;
        angleY = Mathf.Clamp(angleY, minAngleY, maxAngleY);

        // set the quaternion rotation
        Quaternion rotation = Quaternion.Euler(new Vector3(angleY, angleX, 0));

        // set the camera's position relative to the target
        transform.position = target.position + rotation * orbitRadius;

        // rotate the camera to refocus
        transform.rotation = rotation;
    }
}
