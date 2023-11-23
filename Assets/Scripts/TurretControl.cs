using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using Unity.VisualScripting;
public class TurretControl : MonoBehaviour
{
    public int currentLife;
    public int maxLife;
    public int minLife;
    public int damageReward = 5;
    public int killReward = 10;

    public float attackRange;
    private NavMeshAgent agent;
    private PlayerController target;
    [Header("Patrol")]
    private WeaponController weapon;
    public int point = 5;

    public void DamageEnemy(int quantity)
    {
        currentLife -= quantity;
        if (currentLife <= 0)
        {
            GameManager.instance.UpdateScore(point);
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        //DataScriptable

        currentLife = maxLife;
        weapon = GetComponent<WeaponController>();
        target = FindObjectOfType<PlayerController>();

        //agent.SetDestination(target.transform.position);
    }

    private void Update()
    {
        SearchEnemy();

        //agent.SetDestination(target.transform.position);
    }

    private void SearchEnemy()
    {
        RaycastHit hit;
        Vector3 direction = target.transform.position - transform.position;

        if (Physics.Raycast(transform.position, direction, out hit))
        {
            if (hit.collider.CompareTag("Player") && hit.distance <= 10f)
            {
                transform.LookAt(target.transform.position);
                if (hit.distance <= 10f)
                {
                    if (weapon.CanShoot())
                        weapon.Shoot();
                }


            }


        }
    }
}
