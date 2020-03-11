using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {

    public SceneFade fadeObject;

    void OnTriggerEnter(Collider other) {
        // "if layer == 'Player'"
        if (other.gameObject.CompareTag("Player")) {
            fadeObject.FadeToLevel();
        }
    }
}
