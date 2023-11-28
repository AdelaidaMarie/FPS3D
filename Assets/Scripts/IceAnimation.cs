using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceAnimation : MonoBehaviour
{
    private Animator iceAnim;
    public bool activated;
    private void Awake()
    {
        iceAnim = GetComponentInChildren<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {

        activated = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator Burn(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fireball") && !activated)
        {
            Debug.Log("DAB");
            activated = true;
            iceAnim.SetBool("Hot", true);
            StartCoroutine(Burn(5f));
        }
    }
}
