using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript1 : MonoBehaviour
{
    public GameObject end;
    public GameObject text;

    // Start is called before the first frame update


    // Update is called once per frame

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            gameObject.SetActive(false);
            end.SetActive(true);
            text.SetActive(false);

        }
    }
}
