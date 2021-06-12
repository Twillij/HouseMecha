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

    // Start is called before the first frame update
    private void Start()
    {
        orbitRadius = this.transform.position - target.position;
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
    
    // Update is called once per frame
    private void Update()
    {
        angleX += Input.GetAxis("Mouse X") * sensitivityX * Time.deltaTime;
        angleY -= Input.GetAxis("Mouse Y") * sensitivityY * Time.deltaTime;
        angleY = Mathf.Clamp(angleY, minAngleY, maxAngleY);
    }

    private void FixedUpdate()
    {
        // set the quaternion rotation
        Quaternion rotation = Quaternion.Euler(new Vector3(angleY, angleX, 0) );

        // set the camera's position relative to the target
        this.transform.position = target.position + rotation * orbitRadius;

        // rotate the camera to refocus
        this.transform.rotation = rotation;
    }
}
