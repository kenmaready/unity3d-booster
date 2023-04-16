using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flight : MonoBehaviour
{

    Rigidbody rb;
    float thrust = 50f;
    float rotateSpeed = 100f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotate();
    }

    void ProcessThrust() {
        if (Input.GetKey(KeyCode.Space)) {
            rb.AddRelativeForce(Vector3.up * thrust * Time.deltaTime, ForceMode.Impulse);
        }
    }

    void ProcessRotate() {
        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) {
            rb.freezeRotation = true;
            transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
            rb.freezeRotation = false;
        }

        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow)) {
            rb.freezeRotation = true;
            transform.Rotate(Vector3.back * rotateSpeed * Time.deltaTime);
            rb.freezeRotation = false;
        }
    }
}
