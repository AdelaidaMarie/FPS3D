using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpforce;
    private Rigidbody rb;
    public float mouseSensibility;
    public float maxViewX;
    public int currentLives;

    public int maxLife;
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
    public float maxStamina = 100;
    public float minStamina = 0;
    public float currentStamina = 100;
    private float sprintSpeed = 10;
    bool isSprint;
    public ParticleSystem Thundar;
    [HideInInspector]public Cristal cristal1;
    [HideInInspector]public Cristal cristal2;
    [HideInInspector]public Cristal cristal3;
    public GameObject Liz;
    public GameObject LizHurt;
    public GameObject explosion;
    public GameObject elemental;
    public ParticleSystem effect;
    public Transform outPosition;
    bool resting;
    public int point = 10;
    public GameManager gameManager;
    private bool swing;
    bool end;
    public Animator endScene;
    public GameObject HUD;
    bool stop;
    public GameObject ButtonEv;
    public int MaxLives { get => maxLife; set => maxLife = value; }
    public int MaxMana { get => maxMana; set => maxMana = value; }
    public float MaxStamina { get => maxStamina; set => maxStamina = value; }
    private void Awake()
    {
        
        Time.timeScale = 1f;
        rb = GetComponent<Rigidbody>();
        camera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        cristal1 = Fire.GetComponent<Cristal>();
        cristal2 = Ice.GetComponent<Cristal>();
        cristal3 = Thunder.GetComponent<Cristal>();
        gameManager = gameManager.GetComponent<GameManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
        swing = false;
        isSprint = false;
        CameraView();
        HUDController.instance.UpdateHealthBar(MaxLives);
        HUDController.instance.UpdateManaBar(MaxMana);
        HUDController.instance.UpdateStaminaBar(MaxStamina);
        StartCoroutine(StartCine(3f));
    }
    /*private void Dashing()
    {
        rb.AddForce(transform.forward * dashSpeed, ForceMode.Impulse);
        effect.Play();
    }*/
    /// <summary>
    /// Es cine
    /// </summary>
    IEnumerator StartCine(float time)
    {
        end = true;
        endScene.Play("Fade5");
        HUD.SetActive(false);
        yield return new WaitForSeconds(time);
        end = false;
        HUD.SetActive(true);
        Debug.Log("Start");
    }
    IEnumerator EndCine(float time)
    {
        end = true;
        HUD.SetActive(false);
        endScene.Play("Fade4");
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    IEnumerator SpawnTimer(float time)
    {

        
            yield return new WaitForSeconds(time);
            currentMana = currentMana + regMana;
        stop = false;
        yield break;

            
    }
    // Update is called once per frame
    void Update()
    {
        if (currentLives <= minLife)
        {
            end = true;
        }
        if(currentStamina > maxStamina)
        {
            currentStamina = maxStamina;
        }
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
            stop = true;
        }
        Hurt();
        if(currentMana < 10)
        {
            
            if (!stop)
            {
                StartCoroutine(SpawnTimer(5f));
                stop = true;
            }
        }
        if(currentLives > maxLife)
        {
            currentLives = maxLife;
        }
        HUDController.instance.UpdateHealthBar(currentLives);
        HUDController.instance.UpdateManaBar(currentMana);
        HUDController.instance.UpdateStaminaBar(currentStamina);
        if (!end && Time.timeScale == 1f)
        {
            MovePlayer();
        }
        if (Input.GetButtonDown("Jump") && !end)
        {
            Jump();
        }
        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 10 && !swing && !end)
        {
            isSprint = true;
            
            
        } else {
           
            isSprint = false;
        }
        if (isSprint)
        {
            speed = sprintSpeed;
            currentStamina -= 10 * Time.deltaTime;
            if (currentStamina < 50)
            {
                isSprint = false;
            }
        }
        else
        {
            isSprint = false;
            speed = 3;
            currentStamina += 5 * Time.deltaTime;
        }
        if (Time.timeScale == 1f && !end)
        {
            CameraView();
        }
        if (Input.GetButtonDown("Fire1") && !end)
        { 
                if (Fire.activeSelf && currentMana > 3)
                {
                    currentMana = currentMana - cristal1.mana;

                    cristal1.Shoot();
                }
                else if (Ice.activeSelf && currentMana > 2)
                {
                    currentMana = currentMana - cristal2.mana;

                    cristal2.Shoot();
                }
                else if (Thunder.activeSelf && currentMana > 8)
                {
                Thundar.Play();
                    currentMana = currentMana - cristal3.mana;

                    cristal3.Shoot();
                
                }
        }
        if (Input.GetButtonDown("Fire2") && !end)
        {
            if (Fire.activeSelf && currentMana > 3)
            {
                currentMana = currentMana - cristal1.mana;
                Instantiate(explosion, outPosition.position + new Vector3(1, 0, 0), Quaternion.identity);
                
            }
            else if (Ice.activeSelf && currentMana > 10)
            {
                currentMana = currentMana - 10;
                Instantiate(elemental, outPosition.position + new Vector3(3, 2, 0), Quaternion.identity);
            }
            else if (Thunder.activeSelf && currentMana > 1)
            {
                currentMana--;
                //Dashing();
                //Instantiate(TeleBullet, outPosition.position + new Vector3(1, 0, 0), Quaternion.identity);
                cristal3.Shoot2();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && !end)
        {
            Fire.SetActive(true);
            Ice.SetActive(false);
            Thunder.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && !end)
        {
            Fire.SetActive(false);
            Ice.SetActive(true);
            Thunder.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && !end)
        {
            Fire.SetActive(false);
            Ice.SetActive(false);
            Thunder.SetActive(true);
        }
        if (swing)
        {
            speed = 2f;
        }
        if (Input.GetKeyDown(KeyCode.F) && resting && gameManager.Score >= 11)
        {
            GameManager.instance.UpdateScore(-point);
            currentLives = maxLife;
            currentMana = MaxMana;
            currentStamina = MaxStamina;
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
        if(other.gameObject.tag == "Water")
        {
            swing = true;
        }
        if (other.gameObject.tag == "RestArea")
        {
            resting = true;
        }
        if (other.gameObject.tag == "EndPoint")
        {
            StartCoroutine(EndCine(3f));
        }
        if (other.gameObject.tag == "Button")
        {
            ButtonEv.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            swing = false;
        }
        if (other.gameObject.tag == "RestArea")
        {
            resting = false;
        }
        if (other.gameObject.tag == "Button")
        {
            ButtonEv.SetActive(false);
        }
    }

}
