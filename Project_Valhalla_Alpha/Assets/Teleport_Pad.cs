using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Teleport_Pad : MonoBehaviour
{
    //Private variables
    private MeshRenderer hide_pad;

    public int sceneIndex;


    // Start is called before the first frame update
    void Start()
    {
        hide_pad = this.gameObject.GetComponent<MeshRenderer>();
    }

    /// <summary>
    /// OnTriggerEnter:
    ///     - disables mesh renderer of this gameobject.
    ///     - hides a public object. 
    /// </summary>
    /// <param name="padEnter"> creates collider between this obecjc and player</param>
    void OnTriggerEnter(Collider teleport_entered)
    {
        if (teleport_entered.tag == "Player")
        {
            hide_pad.enabled = false;
            SceneManager.LoadScene(sceneIndex);
        }

    }

    /// <summary>
    /// OnTriggerExit:
    ///     - enables mesh renderer of this gameobject.
    ///     - shows a public object. 
    /// </summary>
    /// <param name="padExit"></param>
    void OnTriggerExit(Collider teleport_exited)
    {
        if (teleport_exited.tag == "Player")
        {
            hide_pad.enabled = true;
            Debug.Log("bye");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
