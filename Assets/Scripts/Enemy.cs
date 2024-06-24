using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
public class Enemy : MonoBehaviour
{
    [Header("Stars")]
    public int CurrentHP;
    public int MaxHP;
    public int ScoreToGive;

    [Header("Movement")]
    public float moveSpeed;
    public float attackRange;
    public float yPathOffset;

    private List<Vector3> path;

    private Weapon weapon;
    private GameObject target;


    private void Start()
    {
        weapon = GetComponent<Weapon>();
        target = FindObjectOfType<Player>().gameObject;

        InvokeRepeating("UpdatePath", 0.0f, 0.5f);
    }
    private void Update()
    {
        float dist = Vector3.Distance(transform.position,target.transform.position);
        if(dist <= attackRange)
        {
            if(weapon.CanShoot())
            {
                weapon.Shoot();
            }
        }else
        {
            ChasePlayer();
        }

        
    }


    void ChasePlayer()
    {
        if (path.Count == 0)
            return;
        {
            
        }
        transform.position = Vector3.MoveTowards(transform.position, path[0] + new Vector3(0, yPathOffset, 0), moveSpeed * Time.deltaTime);
        if(transform.position == path[0]+new Vector3(0,yPathOffset,0))
        {
            path.RemoveAt(0);
        }
    }
    void UpdatePath()
    {
        NavMeshPath navMeshPath = new NavMeshPath();
        NavMesh.CalculatePath(transform.position, target.transform.position, NavMesh.AllAreas, navMeshPath);

        path = navMeshPath.corners.ToList(); 
    }
}
