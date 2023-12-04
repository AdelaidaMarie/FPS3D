using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Music : MonoBehaviour
{
    private void Awake()
    {

    }
    // Start is called before the first frame update
    public AudioSource audioSource;
    public AudioSource audioSource2;
    
    void Start()
    {
        audioSource.Play();
        audioSource2.Play();
        StartCoroutine(Ambience());
    }
    IEnumerator Ambience()
    {
        yield return new WaitForSeconds(7f);
        audioSource2.Stop();
        yield return new WaitForSeconds(5f);
        audioSource.Stop();
    }
}
