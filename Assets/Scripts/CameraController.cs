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
    public float zoomSpeed = 100;
    public Vector3 zoomInClamp = Vector3.zero;
    public Vector3 zoomOutClamp = Vector3.zero;
    public bool zoomLockX = true;
    public bool zoomLockY = true;
    public bool zoomLockZ = false;

    public Transform target;

    private Vector3 orbitRadius;
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

    private void Zoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        // zoom in
        if (scroll > 0.0f)
        {
            if (!zoomLockX)
            {
                orbitRadius.x += zoomSpeed * Time.deltaTime;
                orbitRadius.x = Mathf.Min(orbitRadius.x, zoomInClamp.x);
            }

            if (!zoomLockY)
            {
                orbitRadius.y += zoomSpeed * Time.deltaTime;
                orbitRadius.y = Mathf.Min(orbitRadius.y, zoomInClamp.y);
            }

            if (!zoomLockZ)
            {
                orbitRadius.z += zoomSpeed * Time.deltaTime;
                orbitRadius.z = Mathf.Min(orbitRadius.z, zoomInClamp.z);
            }
        }
        // zoom out
        else if (scroll < 0.0f)
        {
            if (!zoomLockX)
            {
                orbitRadius.x -= zoomSpeed * Time.deltaTime;
                orbitRadius.x = Mathf.Max(orbitRadius.x, zoomOutClamp.x);
            }

            if (!zoomLockY)
            {
                orbitRadius.y -= zoomSpeed * Time.deltaTime;
                orbitRadius.y = Mathf.Max(orbitRadius.y, zoomOutClamp.y);
            }

            if (!zoomLockZ)
            {
                orbitRadius.z -= zoomSpeed * Time.deltaTime;
                orbitRadius.z = Mathf.Max(orbitRadius.z, zoomOutClamp.z);
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

        Zoom();
    }
}
