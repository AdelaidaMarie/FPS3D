using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using Unity.VisualScripting;

public class ElementalScript : MonoBehaviour
{
    private WeaponController weapon;
    private EnemyController target;
    private NavMeshAgent agent;
    public float attackRange;
    public float followRange;
    public Transform[] points;
    private int desPoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        weapon = GetComponent<WeaponController>();
        target = FindObjectOfType<EnemyController>();
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        GotoNextPoint();
        StartCoroutine(End(20f));
    }
    private IEnumerator End(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        SearchEnemy();
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();

    }
    private void GotoNextPoint()
    {
        if (points.Length == 0)

            return;
        agent.destination = points[desPoint].position;
        desPoint = (desPoint + 1) % points.Length;
    }
    private void SearchEnemy()
    {
        RaycastHit hit;
        Vector3 direction = target.transform.position - transform.position;
        
        if (Physics.Raycast(transform.position, direction, out hit))
        {
            if (hit.collider.CompareTag("Enemy") && hit.distance <= 10f)
            {
                agent.SetDestination(target.transform.position);
                agent.stoppingDistance = 3f;
                transform.LookAt(target.transform.position);
                if (hit.distance <= 9f)
                {
                    if (weapon.CanShoot())
                        weapon.Shoot();
                }
            }

            else
            {
                agent.stoppingDistance = 1f;

            }

        }


    }

}
