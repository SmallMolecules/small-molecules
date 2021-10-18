using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/** @brief Mouse Look

    This class manages a first person field of view inside the simulation. 
    @author Matthew Vereker
    @date September 2021
    */
public class MouseLook : MonoBehaviour
{
    /*Sets the mouse sensitivity to a default value and controls the speed of the mouse*/
    public float mouseSensitivity = 100f;

    public Transform playerBody;

    float xRotation = 0f;

    void Start()
    {
    	Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void Update()
    {
    	 /*Will get the input of the mouse coordinates in the simulation*/
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        /*Rotates independant of the frame rate*/
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
 