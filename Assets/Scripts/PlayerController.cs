using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI speedometerText;
    [SerializeField] private TextMeshProUGUI rpmText;
    [SerializeField] private GameObject centerOfMass;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera secondaryCamera;
    [SerializeField] private string inputID;
    [SerializeField] private float horsePower = 0.0f;
    [SerializeField] private float turnSpeed = 30.0f;
    [SerializeField] private float speed;
    [SerializeField] private float rpm;
    [SerializeField] private List<WheelCollider> wheels;
    [SerializeField] private int wheelsOnGround;

    private Rigidbody playerRigidbody;
    private float horizontalInput;
    private float forwardInput;
    private float changeCameraInput;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerRigidbody.centerOfMass = centerOfMass.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // get the user's horizontal input
        horizontalInput = Input.GetAxis("Horizontal" + inputID);

        // get the user's vertical input
        forwardInput = Input.GetAxis("Vertical" + inputID);

        // get the user's change camera input
        changeCameraInput = Input.GetAxis("ChangeCamera");

        if (IsOnGround())
        {
            playerRigidbody.AddRelativeForce(Vector3.forward * forwardInput * horsePower);
            speed = Mathf.Round(playerRigidbody.velocity.magnitude * 3.6f); // 2.237 MPH, 3.6 KPH
            speedometerText.SetText($"Speed: {speed}");

            rpm = Mathf.Round((speed % 30) * 40);
            rpmText.SetText($"RPM: {rpm}");

            // Rotates the car based on horizontal input
            transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);

        }

        if (changeCameraInput == 1)
        {
            mainCamera.enabled = !mainCamera.enabled;
            secondaryCamera.enabled = !secondaryCamera.enabled;
        }
    }

    bool IsOnGround()
    {
        wheelsOnGround = 0;
        foreach (WheelCollider wheel in wheels)
        {
           if (wheel.isGrounded)
            {
                wheelsOnGround++;
            }
        }

        if (wheelsOnGround == 4) { 
            return true;
        } else
        {
            return false;
        }
    }
}
