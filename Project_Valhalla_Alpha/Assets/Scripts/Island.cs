using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour
{
    public bool bFade = false;
    private Color oldColor;
    private Color fadeColor;

    private void Awake() {
        // setup fade colors
        oldColor = gameObject.GetComponent<MeshRenderer>().material.color;
        fadeColor = gameObject.GetComponent<MeshRenderer>().material.color;
        fadeColor.a = 0.5f;
    }

    void Update() {
        if (bFade) {
            Debug.Log("island fade");
            gameObject.GetComponent<MeshRenderer>().material.color = fadeColor;
        } else {
            gameObject.GetComponent<MeshRenderer>().material.color = oldColor;
        }

        bFade = false;
    }
}
