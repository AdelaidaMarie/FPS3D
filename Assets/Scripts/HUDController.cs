using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HUDController : MonoBehaviour
{
    [SerializeField]private Slider healthbar;
    [SerializeField]private Slider manabar;
    [SerializeField] private Slider staminabar;
    public TextMeshProUGUI scoreText;
    [SerializeField] private Image damageFlash;
    [SerializeField] private float damageTime;
    public static HUDController instance;
    private Coroutine dissapearCoroutine;
    public GameObject FailPanel;
    public GameObject GameManager;
    private void Awake()
    {

        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
        healthbar.maxValue = GameObject.Find("Player").GetComponent<PlayerController>().MaxLives;
        manabar.maxValue = GameObject.Find("Player").GetComponent<PlayerController>().MaxMana;
        staminabar.maxValue = GameObject.Find("Player").GetComponent<PlayerController>().MaxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateHealthBar(int currentHealth)
    {
        
        healthbar.value = currentHealth;
        if (currentHealth <= 0)
        {
            healthbar.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.black;
            GameManager.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            FailPanel.SetActive(true);
        }
    }
    public void UpdateManaBar(int currentMana)
    {
        manabar.value = currentMana;
    }
    public void UpdateStaminaBar(float currentStamina)
    {
        staminabar.value = currentStamina;
    }
    public void ShowDamageFlash()
    {
        if (dissapearCoroutine != null)
              StopCoroutine(dissapearCoroutine);
            
        damageFlash.color = Color.white;
            
        dissapearCoroutine = StartCoroutine(DamageDissapear());


    }
    IEnumerator DamageDissapear()
    {
        float alpha = 1.0f;
        while(alpha > 0.0f)
        {
            alpha -= (1.0f / damageTime) * Time.deltaTime;
            damageFlash.color = new Color(1.0f, 1.0f, 1.0f, alpha);
            yield return null;
        }
       
    }
}
