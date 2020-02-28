using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class life_scale : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Player_v3 player;

    [SerializeField] GameObject life1;
    [SerializeField] GameObject life2;
    [SerializeField] GameObject life3;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player.playerHealth == 2)
        {
            life3.SetActive(false);
        }

        if (player.playerHealth == 1)
        {
            life3.SetActive(false);
            life2.SetActive(false);
        }


        if (player.playerHealth == 0)
        {
            life3.SetActive(false);
            life2.SetActive(false);
            life1.SetActive(false);
            
        }

    }
}
