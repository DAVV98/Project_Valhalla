using UnityEngine;
using UnityEngine.SceneManagement;

public class cl_level_fade : MonoBehaviour {

    public Animator animator;

    public void FadeToLevel() {
        animator.SetTrigger("fade_out");
    }

    // OnFadeComplete() change scene
    public void OnFadeComplete() {
        // If on last level, go to menu scene
        // This could be improved by automatically detecting last level in build rather than manually setting it
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            SceneManager.LoadScene(0);
        }
        // By default, go to next scene
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
