using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera mainCamera;
    public Camera secondaryCamera;
    public string inputID;

    private float speed = 20f;
    private float turnSpeed = 45f;
    private float horizontalInput;
    private float forwardInput;
    private float changeCameraInput;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // get the user's horizontal input
        horizontalInput = Input.GetAxis("Horizontal" + inputID);

        // get the user's vertical input
        forwardInput = Input.GetAxis("Vertical" + inputID);

        // get the user's change camera input
        changeCameraInput = Input.GetAxis("ChangeCamera");

        // Moves the car forward based on vertical input
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);

        // Rotates the car based on horizontal input
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);

        if (changeCameraInput == 1)
        {
            mainCamera.enabled = !mainCamera.enabled;
            secondaryCamera.enabled = !secondaryCamera.enabled;
        }
    }
}
