using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cristal : MonoBehaviour
{
    
    public CristalData cristaldata;
    [HideInInspector]public int mana;
    [HideInInspector]public float rate;
    [HideInInspector]public float maxDistance;
    [HideInInspector]public int damage;
    [HideInInspector]public float ballSpeed;
    [HideInInspector]public float ballTime;
    private ObjectPool poolObjects;
    public Transform outPosition;
    public GameObject TeleBullet;
    public AudioSource shootSound;
    private void Awake()
    {
            poolObjects = GetComponent<ObjectPool>();

    }
    public void Start()
    {
        mana = cristaldata.useMana;
        rate = cristaldata.rate;
        maxDistance = cristaldata.maxDistance;
        damage = cristaldata.damage;
        ballSpeed = cristaldata.ballSpeed;
        ballTime = cristaldata.ballTime;
 
    }
    public void Shoot()
    {
        shootSound.Play();
        GameObject ball = poolObjects.GetGameObject();
        ball.transform.position = outPosition.position;
        ball.transform.rotation = outPosition.rotation;
        ball.GetComponent<BallController>().Damage = damage;
        ball.GetComponent<Rigidbody>().velocity = outPosition.forward *
            ballSpeed;

        /* GameObject ball = poolObjects.GetGameObject();
         ball.transform.position = outPosition.position;
         ball.transform.rotation = outPosition.rotation;
         ball.GetComponent<BallController>().Damage = damage;
         ball.GetComponent<Rigidbody>().velocity = outPosition.forward *
             ballSpeed;*/
    }
    //Only for thunder crystal
    public void Shoot2()
    {
        TeleBullet = Instantiate(TeleBullet);

        TeleBullet.transform.position = outPosition.position;
        TeleBullet.transform.rotation = outPosition.rotation;
        TeleBullet.GetComponent<Rigidbody>().velocity = outPosition.forward *
            ballSpeed;
    }
}
