using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    AudioSource sound;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip landingSFX;

    bool isTransitioning = false;


    private void Awake() {
        sound = GetComponent<AudioSource>();
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
                StartCrashSequence();
                break;
        }
    }

    void StartCompleteSequence() {
        isTransitioning = true;
        sound.PlayOneShot(landingSFX);
        StartCoroutine(LoadNextLevel());
    }

    void StartCrashSequence() {
        isTransitioning = true;
        sound.PlayOneShot(crashSFX);
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
