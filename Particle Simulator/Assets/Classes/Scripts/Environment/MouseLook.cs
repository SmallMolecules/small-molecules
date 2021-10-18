using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/** @brief Mouse Look

    This class manages a first person field of view inside the simulation. 
    @author Matthew Vereker
    @date October 2021
    */
public class MouseLook : MonoBehaviour
{
    /*Sets the mouse sensitivity to a default value and controls the speed of the mouse*/
    public float mouseSensitivity = 100f;

    /*Reference to the transform of the player*/
    public Transform playerBody;

    /* The x rotation of the player */
    float xRotation = 0f;

    /**
    \see @link https://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
    */
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    /**
    \see @link https://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
    */
    void Update()
    {
        //Will get the input of the mouse coordinates in the simulation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //Rotates independant of the frame rate
        xRotation -= mouseY;

        //Clamp rotation so we cant over rotate and go behind the player
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Reference for local camera
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
