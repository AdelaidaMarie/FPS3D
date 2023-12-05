using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public GameObject text;
    public bool active;
    public GameObject imageMission;
    public AudioSource audioSource;

    // Start is called before the first frame update


    // Update is called once per frame

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            active = true;
            gameObject.SetActive(false);
            text.SetActive(false);
            imageMission.SetActive(false);
            audioSource.Play();

        }
    }
}
