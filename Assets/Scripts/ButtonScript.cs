using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public GameObject text;
    public bool active;
    // Start is called before the first frame update
    

    // Update is called once per frame
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            gameObject.SetActive(false);
            text.SetActive(false);
            active = true;
        }
    }
}
