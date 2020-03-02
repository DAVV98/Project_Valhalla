using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_anim : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Animator anim;

    [SerializeField] ProjectileTrigger trigger;
    //[SerializeField] Animator anim;


    void Awake()
    {


    }

    // Update is called once per frame
    private void Update()
    {
        if (trigger.Active == true)
        {
            anim.SetBool("open", true);
        }
    }
}

   
