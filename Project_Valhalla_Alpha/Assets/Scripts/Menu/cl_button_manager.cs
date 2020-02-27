using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cl_button_manager : MonoBehaviour {
    
    // StartGame() was previously used before the cl_level_change and cl_level_fade were implemented
    // Called when start button is pressed
    //public void StartGame() {
    //    SceneManager.LoadScene(1);
    //}

    public void ExitGame() {
        Debug.Log("Exit Game");
        Application.Quit();
    }
}
