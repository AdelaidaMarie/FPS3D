using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(End(1f));
    }
    private IEnumerator End(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
