using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wscript : MonoBehaviour
{
    
    IResiveHitRedirect toredirect;

    [SerializeField] GameObject player;

    [SerializeField]
    HitType hitType;

    // Start is called before the first frame update
    void Awake()
    {
        toredirect = GetComponentInParent<IResiveHitRedirect>();
    }

    private void OnTriggerEnter(Collider other)
    {
        toredirect.hitredirect(other, hitType);
        
    }
}

public interface IResiveHitRedirect
{
    void hitredirect(Collider other, HitType hit);


    

}

public enum HitType
{
    attackarea,
    weapon
}

