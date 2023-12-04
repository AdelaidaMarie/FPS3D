using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RestScript : MonoBehaviour
{
    public GameObject restText;
    
    // Start is called before the first frame update
    void Start()
    {
        //player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            restText.SetActive(true);
            /*if(Input.GetKeyDown(KeyCode.E))
            {
                player.currentLives = player.maxLife;
                player.currentMana = player.MaxMana;
                player.currentStamina = player.MaxStamina;
            }*/
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            restText.SetActive(false);
        }
    }
}
