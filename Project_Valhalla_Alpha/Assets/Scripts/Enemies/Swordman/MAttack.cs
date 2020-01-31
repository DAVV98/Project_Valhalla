using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MAttack : MonoBehaviour,IResiveHitRedirect
{
    Animator anim;

    [SerializeField] GameObject sword;
    [SerializeField] GameObject attackarea;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmosSelected()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 4;
        Gizmos.DrawRay(transform.position, direction);
    }

    public void hitredirect(Collider other, HitType hit)
    {
        if (hit == HitType.attackarea)
        {
            anim.SetTrigger("attack");
            sword.SetActive(true);
            attackarea.SetActive(false);
        }
    }

    public void onAttackFinish()
    {
        sword.SetActive(false);
        attackarea.SetActive(true);
    }
}
