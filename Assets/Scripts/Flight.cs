using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flight : MonoBehaviour
{

    [SerializeField] AudioClip engineThrustSFX;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftBoosterParticles;
    [SerializeField] ParticleSystem rightBoosterParticles;
    
    Rigidbody rb;
    AudioSource sound;
    
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
            
            if (!sound.isPlaying) {
                sound.PlayOneShot(engineThrustSFX);
            }
            if (!mainEngineParticles.isPlaying) {
                mainEngineParticles.Play();
            }

        } else {
            if (sound.isPlaying) { sound.Stop(); }
            if (mainEngineParticles.isPlaying) { mainEngineParticles.Stop(); }
        }
    }

    void ProcessRotate() {
        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) {
            if (!leftBoosterParticles.isPlaying) {
                leftBoosterParticles.Play();
            }

            rb.freezeRotation = true;
            transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
            rb.freezeRotation = false;
        }

        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow)) {
            if (!rightBoosterParticles.isPlaying) {
                rightBoosterParticles.Play();
            }
            rb.freezeRotation = true;
            rightBoosterParticles.Play();
            transform.Rotate(Vector3.back * rotateSpeed * Time.deltaTime);
            rb.freezeRotation = false;
        }

        if (leftBoosterParticles.isPlaying && !Input.GetKey(KeyCode.LeftArrow)) {
            leftBoosterParticles.Stop();
        }

        if (rightBoosterParticles.isPlaying && !Input.GetKey(KeyCode.RightArrow)) {
            rightBoosterParticles.Stop();
        }

    }

    void DeRotate() {
            Quaternion currRotation = transform.rotation;
            currRotation.x = 0;
            currRotation.y = 0;
            transform.rotation = currRotation;
    }
}
