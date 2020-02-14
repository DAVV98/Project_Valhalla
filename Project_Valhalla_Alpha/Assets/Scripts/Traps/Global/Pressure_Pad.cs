using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pressure_Pad : MonoBehaviour
{
    [Header("Object_To_Hide")]
    public GameObject hiddenObject;

    //Private variables
    private MeshRenderer hider;

    // Start is called before the first frame update
    void Start()
    {
       //gets mesh renderer of this gameobject.
       hider = this.gameObject.GetComponent<MeshRenderer>();
    }

    /// <summary>
    /// OnTriggerEnter:
    ///     - disables mesh renderer of this gameobject.
    ///     - hides a public object. 
    /// </summary>
    /// <param name="padEnter"> creates collider between this obecjc and player</param>
    void OnTriggerEnter(Collider padEnter)
    {
        if(padEnter.tag == "Pushable")
        {
            hider.enabled = false;
            hiddenObject.SetActive(false);
          
        }
        
    }

    
    /// <summary>
    /// OnTriggerExit:
    ///     - enables mesh renderer of this gameobject.
    ///     - shows a public object. 
    /// </summary>
    /// <param name="padExit"></param>
    void OnTriggerExit(Collider padExit)
    {
        if (padExit.tag == "Pushable")
        {
            hider.enabled = true;
            hiddenObject.SetActive(true);
           
        }

    }
    
}
