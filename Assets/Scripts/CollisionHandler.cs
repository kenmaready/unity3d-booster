using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    AudioSource sound;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip landingSFX;

    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem landingParticles;

    bool isTransitioning = false;
    bool disableCollisions = false;


    private void Awake() {
        sound = GetComponent<AudioSource>();
    }

    private void Update() {
        CheckCheatKeys();
    }

    void CheckCheatKeys() {
        if (Input.GetKeyDown(KeyCode.C)) {
            disableCollisions = !disableCollisions;
        }

        if (Input.GetKeyDown(KeyCode.L)) {
            StartCoroutine(LoadNextLevel());
        }
    }

    private void OnCollisionEnter(Collision other) {

        if (isTransitioning) {
            return;
        }

        switch (other.gameObject.tag) {
            case "Friendly":
                break;
            case "Finish":
                StartCompleteSequence();
                break;
            default:
                if (disableCollisions) { break; }
                ContactPoint contactPoint = other.GetContact(0);
                StartCrashSequence(contactPoint);
                break;
        }
    }

    void StartCompleteSequence() {
        isTransitioning = true;
        sound.PlayOneShot(landingSFX);
        landingParticles.Play();
        StartCoroutine(LoadNextLevel());
    }

    void StartCrashSequence(ContactPoint pos) {
        isTransitioning = true;
        sound.PlayOneShot(crashSFX);
        Debug.Log("Position of Collision: " + pos.point);
        crashParticles.Play();
        GetComponent<Flight>().enabled = false;
        StartCoroutine(ReloadLevel());
    }

    IEnumerator ReloadLevel() {
        yield return new WaitForSeconds(1.5f);

        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }

    IEnumerator LoadNextLevel() {
        yield return new WaitForSeconds(1.5f);

        int sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        Debug.Log("SceneCount: " + SceneManager.sceneCountInBuildSettings);
        if (sceneIndex >= SceneManager.sceneCountInBuildSettings) {
            sceneIndex = 0;
        }
        SceneManager.LoadScene(sceneIndex);
    }

}
