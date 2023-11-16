using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform outPosition;
    public int currentAmmo;
    public int maxAmmo;
    public bool infiniteAmmo;
    public float ballSpeed;
    public float ballTime;
    public float shootRate;
    private ObjectPool poolObjects;
    private float lastShootTime;
    public int damage;
    private bool isPlayer;

    private void Awake()
    {
        if(GetComponent<PlayerController>())
        
            isPlayer = true;

            poolObjects = GetComponent<ObjectPool>();
        
    }
    public bool CanShoot()
    {
        if (Time.time - lastShootTime > shootRate)
        
            if (currentAmmo > 0 || infiniteAmmo)
            
                return true;
            
            return false;
        
        
    }
    public void Shoot()
    {

        lastShootTime = Time.time;
        if(!infiniteAmmo) currentAmmo--;
        GameObject ball = poolObjects.GetGameObject();
        ball.transform.position = outPosition.position;
        ball.transform.rotation = outPosition.rotation;
        ball.GetComponent<BallController>().Damage = damage;
        ball.GetComponent<Rigidbody>().velocity = outPosition.forward *
            ballSpeed;
        /*if (isPlayer)
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            Vector3 targetPoint;
            if (Physics.Raycast(ray, out hit))
            {
                targetPoint = hit.point;
            }
            else
            {
                targetPoint = ray.GetPoint(5);
                ball.GetComponent<RigidBody>().velocity = (targetPoint - ball.transform.position).normalized * ballSpeed;
            }
        } else
        {

        }*/
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
