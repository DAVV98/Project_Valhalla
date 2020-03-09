using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wscript : MonoBehaviour
{
    
    IResiveHitRedirect toredirect;

    

    [SerializeField]
    HitType hitType;

    // Start is called before the first frame update
    void Awake()
    {
        toredirect = GetComponentInParent<IResiveHitRedirect>();
    }

    private void OnTriggerEnter(Collider Player)
    {
        if (CompareTag("Player") == true)
        {
            toredirect.hitredirect(Player, hitType);
        }
    }
}

public interface IResiveHitRedirect
{
    void hitredirect(Collider Player, HitType hit);


    

}

public enum HitType
{
    attackarea,
    weapon
}

