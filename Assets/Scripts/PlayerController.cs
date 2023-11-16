using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpforce;
    private Rigidbody rb;
    public float mouseSensibility;
    public float maxViewX;
    public int currentLives;

    [SerializeField] private int maxLife;
    public int minLife;
    public float minViewX;
    private float rotationX;
    private Camera camera;
    public int regMana = 5;
    [SerializeField] private GameObject Fire;
    [SerializeField] private GameObject Ice;
    [SerializeField] private GameObject Thunder;
    public int currentMana;
    public int maxMana;
    public int minMana;
    public ParticleSystem Thundar;
    [HideInInspector]public Cristal cristal1;
    [HideInInspector]public Cristal cristal2;
    [HideInInspector]public Cristal cristal3;
    public GameObject Liz;
    public GameObject LizHurt;
    public int MaxLives { get => maxLife; set => maxLife = value; }
    public int MaxMana { get => maxMana; set => maxMana = value; }
    private void Awake()
    {
        
        rb = GetComponent<Rigidbody>();
        camera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        cristal1 = Fire.GetComponent<Cristal>();
        cristal2 = Ice.GetComponent<Cristal>();
        cristal3 = Thunder.GetComponent<Cristal>();
    }
    // Start is called before the first frame update
    void Start()
    {
        CameraView();
        HUDController.instance.UpdateHealthBar(MaxLives);
        HUDController.instance.UpdateManaBar(MaxMana);
        
    }
    private IEnumerator SpawnTimer(float time)
    {

        while (true)
        {
            yield return new WaitForSeconds(time);
            currentMana = currentMana + regMana;
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        Hurt();
        if(currentMana < 10)
        {
            StartCoroutine(SpawnTimer(10f));
        }
        else if(currentMana > maxMana)
        {
            currentMana = maxMana;
            StopAllCoroutines();
        }
        else 
        {
            StopAllCoroutines();
        }
        if(currentLives > maxLife)
        {
            currentLives = maxLife;
        }
        HUDController.instance.UpdateHealthBar(currentLives);
        HUDController.instance.UpdateManaBar(currentMana);
        MovePlayer();
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        CameraView();
        if (Input.GetButtonDown("Fire1"))
        { 
                if (Fire.active && currentMana > 3)
                {
                    currentMana = currentMana - cristal1.mana;

                    cristal1.Shoot();
                }
                else if (Ice.active && currentMana > 2)
                {
                    currentMana = currentMana - cristal2.mana;

                    cristal2.Shoot();
                }
                else if (Thunder.active && currentMana > 8)
                {
                Thundar.Play();
                    currentMana = currentMana - cristal3.mana;

                    cristal3.Shoot();
                
                }
        }
        if (Input.GetButtonDown("Fire2"))
        {
            float flash = 100f;
            float x = Input.GetAxis("Horizontal");
            Vector3 dash = (transform.right * x) * flash;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Fire.SetActive(true);
            Ice.SetActive(false);
            Thunder.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Fire.SetActive(false);
            Ice.SetActive(true);
            Thunder.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Fire.SetActive(false);
            Ice.SetActive(false);
            Thunder.SetActive(true);
        }
    }
    
        
    
    /// <summary>
    /// Player Movement
    /// </summary>
    private void MovePlayer()
    { 
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 direction = (transform.right * x + transform.forward * z).normalized * speed;
        direction.y = rb.velocity.y;
        rb.velocity = direction;
    }
    private void Hurt()
    {
        if (currentLives > 5)
        {
            Liz.SetActive(true);
            LizHurt.SetActive(false);
        }
        else if(currentLives <= 5)
        {
            Liz.SetActive(false);
            LizHurt.SetActive(true);
        }
    }
    /// <summary>
    /// Jump Action
    /// </summary>
    private void Jump()
    {
        //Throw a ray down
        Ray ray = new Ray(transform.position, Vector3.down);
        //if the ray collide with something at 1.1m then force up
        if(Physics.Raycast(ray, 1.1f))
        {
            rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
        }
    }
    /// <summary>
    /// Camera Rotation view with mouse
    /// </summary>
    private void CameraView()
    {
        //Take Get from Mouse Input X and Y
        float y = Input.GetAxis("Mouse X") * mouseSensibility;
        rotationX += Input.GetAxis("Mouse Y") * mouseSensibility;
        // Cut x rotation
        rotationX = Mathf.Clamp(rotationX, minViewX, maxViewX);
    // Rotate the camera
    camera.transform.localRotation = Quaternion.Euler(-rotationX, 0, 0);
        transform.eulerAngles += Vector3.up * y;
        if(Time.timeScale == 0f)
        {
            camera.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    public void DamagePlayer(int quantity)
    {
        currentLives -= quantity;
        
        HUDController.instance.UpdateHealthBar(currentLives);
        HUDController.instance.ShowDamageFlash();
        if(currentLives <= 0)
        {
            Time.timeScale = 0f;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Health" && currentLives < maxLife)
        {
            HUDController.instance.UpdateHealthBar(currentLives);
            currentLives = currentLives + regMana;
            
        }
        if(other.gameObject.tag == "Mana" && currentMana < maxMana)
        {
            HUDController.instance.UpdateManaBar(currentMana);
            currentMana = currentMana + regMana;
        }
    }

}
