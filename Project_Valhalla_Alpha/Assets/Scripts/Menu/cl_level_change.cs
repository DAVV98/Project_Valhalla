using UnityEngine;
using UnityEngine.SceneManagement;

public class cl_level_change : MonoBehaviour {

    public cl_level_fade fadeObject;
    private GameObject player;

    void OnTriggerEnter(Collider other) {
        // "if layer == 'Player'"
        if (other.gameObject.layer == 8) {
            fadeObject.FadeToLevel();
        }
    }
}
