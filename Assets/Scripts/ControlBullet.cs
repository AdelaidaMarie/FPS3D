using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBullet : MonoBehaviour
{
    private PlayerController target;
    private float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Teleport());
        target = FindObjectOfType<PlayerController>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator Teleport()
    {
        yield return new WaitForSeconds(1f);
        target.transform.position = gameObject.transform.position;
        yield return new WaitForSeconds(0.1f);
        gameObject.transform.position = new Vector3(0, -2000, 0);
    }
}
