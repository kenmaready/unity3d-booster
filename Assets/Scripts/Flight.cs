using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flight : MonoBehaviour
{

    [SerializeField] AudioClip engineThrustSFX;
    
    Rigidbody rb;
    AudioSource sound;
    bool isPlaying = false;
    
    float thrust = 50f;
    float rotateSpeed = 100f;
    bool isAlive = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        sound = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotate();
        DeRotate();
    }

    void ProcessThrust() {
        if (Input.GetKey(KeyCode.Space)) {
            rb.AddRelativeForce(Vector3.up * thrust * Time.deltaTime, ForceMode.Impulse);
            
            if (!isPlaying) {
                isPlaying = true;
                sound.PlayOneShot(engineThrustSFX);
            }

        } else if (isPlaying) {
            isPlaying = false;
            sound.Stop();
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

    void DeRotate() {
            Quaternion currRotation = transform.rotation;
            currRotation.x = 0;
            currRotation.y = 0;
            transform.rotation = currRotation;
    }
}
