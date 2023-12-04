using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using Unity.VisualScripting;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    public ParticleSystem partBurn;
    public ParticleSystem partFreeze;
    [SerializeField] public int currentLife;
    [SerializeField] public int maxLife;
    public int minLife;
    public int damageReward = 5;
    public int killReward = 10;
    public float speed;
    public int burning = 1;
    public float attackRange;
    public float followRange;
    public bool alwaysFollow;
    private NavMeshAgent agent;
    private PlayerController target;
    [Header("Patrol")]
    public Transform[] points;
    private int desPoint = 0;
    private WeaponController weapon;
    public int point = 5;
    private Renderer enemyRenderer;
    private bool isFrozen = false;
    public AudioSource roar;
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

        speed = enemyData.speed;
        currentLife = enemyData.maxLife;
        enemyRenderer = GetComponent<Renderer>();
        enemyRenderer.material = enemyData.EnemyMaterial;
        weapon = GetComponent<WeaponController>();
        target = FindObjectOfType<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        GotoNextPoint();
        //agent.SetDestination(target.transform.position);
    }

    private void Update()
    {
        SearchEnemy();
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();

        //agent.SetDestination(target.transform.position);
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
        //if (isFrozen)
        //{
          //  agent.SetDestination(transform.position);
        //}
        if (Physics.Raycast(transform.position, direction, out hit) && !isFrozen)
        {
            if (hit.collider.CompareTag("Player") && hit.distance <= 10f)
            {
                roar.Play();
                agent.SetDestination(target.transform.position);
                agent.stoppingDistance = 3f;
                transform.LookAt(target.transform.position);
                if (hit.distance <= 7f)
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
    IEnumerator Burn(float time)
    {
        partBurn.Play();
        currentLife--;
        yield return new WaitForSeconds(time);
        currentLife = currentLife - burning;
        yield return new WaitForSeconds(time);
        currentLife = currentLife - burning;
        yield return new WaitForSeconds(time);
        currentLife = currentLife - burning;
        partBurn.Stop();
    }
    IEnumerator Frozen(float time)
    {
        partFreeze.Play();
        yield return new WaitForSeconds(time);
        partFreeze.Stop();
        isFrozen = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Fireball"))
        {
            //GameObject particles = Instantiate(impactParticle, transform.position, Quaternion.identity);
            StartCoroutine(Burn(3f));
        }
        if (other.CompareTag("Iceball"))
        {
            isFrozen = true;
            StartCoroutine(Frozen(10f));
        }
    }
}
