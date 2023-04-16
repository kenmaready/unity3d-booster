using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    AudioSource sound;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip landingSFX;


    private void Awake() {
        sound = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other) {
        switch (other.gameObject.tag) {
            case "Friendly":
                Debug.Log("Touched a friendly");
                break;
            case "Finish":
                Debug.Log("Touched landing pad");
                sound.PlayOneShot(landingSFX);
                StartCoroutine(LoadNextLevel());
                break;
            default:
                Debug.Log("Kablooey");
                StartCrashSequence();
                StartCoroutine(ReloadLevel());
                break;
        }
    }

    void StartCrashSequence() {
        GetComponent<Flight>().enabled = false;
        sound.PlayOneShot(crashSFX);
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
