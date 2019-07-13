using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFPS : MonoBehaviour {

    /* Set This Component(Script) to MainCamera */
    
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private Transform playerBody;

    private float xAxisClamp;

    private void Awake()
    {
        LookCursor();
        xAxisClamp = 0.0f;
    }

    private void LookCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
	
	private void Update()
    {
        CameraRotation();
        ViewBobbing();
    }

    private void CameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xAxisClamp += mouseY;

        if (xAxisClamp > 90.0f)
        {
            xAxisClamp = 90.0f;
            mouseY = 0.0f;
            ClampXAxisRotationToValue(270.0f);
        } else
        if (xAxisClamp < -90.0f)
        {
            xAxisClamp = -90.0f;
            mouseY = 0.0f;
            ClampXAxisRotationToValue(90.0f);
        }

        transform.Rotate(Vector3.left * mouseY);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void ClampXAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }

    private void ViewBobbing()
    {
        if (Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.D)) /* Moving */
        {
            if (Input.GetKey(PlayerMove.DushKey)) /* Yes Dush */
            {
                print("Running");
            } else                                /* Not Dush */
            {
                print("Walking");
            }
        }
    }
}
