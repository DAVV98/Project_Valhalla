using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {

    public SceneFade fadeObject;
    private GameObject player;

    void OnTriggerEnter(Collider other) {
        // "if layer == 'Player'"
        if (other.gameObject.layer == 8) {
            fadeObject.FadeToLevel();
        }
    }
}
