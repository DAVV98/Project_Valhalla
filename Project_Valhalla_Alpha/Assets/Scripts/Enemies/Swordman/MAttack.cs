using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MAttack : MonoBehaviour,IResiveHitRedirect
{
    Animator anim;

    [SerializeField] S_movement enemyM;
    [SerializeField] Collider player;

    public float Timer = 3;
    public float IntervalSet;

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

        if (IntervalSet > 0)
        {
            IntervalSet -= Time.deltaTime;
            enemyM.MovementSpeed = -0.5f;
            
        }
        if (IntervalSet <= 0)
        {
            IntervalSet = 0;
            enemyM.MovementSpeed = 3f;
            attackarea.SetActive(true);
        }

    }
    void OnDrawGizmosSelected()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 4;
        Gizmos.DrawRay(transform.position, direction);
    }

    public void hitredirect(Collider player, HitType hit)
    {
        if (hit == HitType.attackarea && CompareTag("player") == true)
        {
            anim.SetTrigger("attack");
            sword.SetActive(true);
            attackarea.SetActive(false);
            IntervalSet = Timer;

        }
    }

    public void onAttackFinish()
    {
       
        sword.SetActive(false);
    }
}


