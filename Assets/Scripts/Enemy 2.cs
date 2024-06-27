using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2: MonoBehaviour
{
    public Player player;
    public float attackDistance;
    public int damage;
    public int health;

    private bool isAttacking;
    private bool isDead;

    public NavMeshAgent agent;
    public Animator anim;
    public GameObject bloodEffect;

    private void Update()
    {
        if (isDead)
            return;

        if(Vector3.Distance(transform.position,player.transform.position) < attackDistance)
        {
            agent.isStopped = true;


            if (!isAttacking)
                Attack();
        }
        else
        {

            agent.isStopped = false;
            agent.SetDestination(player.transform.position);
            anim.SetBool("Running", true);    
        }
    }

    void Attack()
    {
        isAttacking = true;
        anim.SetBool("Running",false);
        anim.SetTrigger("Attack");
        Invoke("TryDamage", 1.2f);
        Invoke("DisableIsAttacking", 2.5f);
    }

    void TryDamage()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < attackDistance)
        {
           player.TakeDamage(damage);
        }
    }

    void DisableIsAttackig()
    {
        isAttacking = false;
    }

    public void TakeDamage(int damageToTake)
    {
        health -= damageToTake;

        if(health < 0)
        {
            isDead = true;
            agent.isStopped = true;
            anim.SetTrigger("Die");
            GetComponent<Collider>().enabled = false;
            GameObject obj = Instantiate(bloodEffect, transform.position, Quaternion.identity);
            Destroy(obj, 20f);
            Destroy(gameObject, 5f);
        }
    }

}
