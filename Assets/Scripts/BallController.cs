using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float activeTime;
    private float shootTime;
    private int damage;
    //public GameObject impactParticleFloor;
    //public GameObject impactParticle;
    public int Damage { get => damage; set => damage = value; }

    private void OnEnable()
    {
        shootTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - shootTime >= activeTime)
        {
            gameObject.SetActive(false);
            OnDisable();
        }
    }
    private void OnDisable()
    {
        gameObject.transform.position = new Vector3(0, -2000, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        if (other.CompareTag("Enemy"))
        {

            //GameObject particles = Instantiate(impactParticle, transform.position, Quaternion.identity);
            other.GetComponent<EnemyController>().DamageEnemy(damage);
            
        }
        else if (other.CompareTag("Player"))
        {

            other.GetComponent<PlayerController>().DamagePlayer(damage);
           
        }
        else if (other.CompareTag("Turret"))
        {

            //GameObject particles = Instantiate(impactParticle, transform.position, Quaternion.identity);
            other.GetComponent<TurretControl>().DamageEnemy(damage);

        }

    }
}
