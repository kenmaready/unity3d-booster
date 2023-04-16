using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    private void OnCollisionEnter(Collision other) {
        switch (other.gameObject.tag) {
            case "Friendly":
                Debug.Log("Touched a friendly");
                break;
            case "Finish":
                Debug.Log("Touched landing pad");
                StartCoroutine(LoadNextLevel());
                break;
            default:
                Debug.Log("Kablooey");
                StartCoroutine(ReloadLevel());
                break;
        }
    }

    IEnumerator ReloadLevel() {
        yield return new WaitForSeconds(1);

        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }

    IEnumerator LoadNextLevel() {
        yield return new WaitForSeconds(1);

        int sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        Debug.Log("SceneCount: " + SceneManager.sceneCountInBuildSettings);
        if (sceneIndex >= SceneManager.sceneCountInBuildSettings) {
            sceneIndex = 0;
        }
        SceneManager.LoadScene(sceneIndex);
    }

}
