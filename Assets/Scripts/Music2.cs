using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Music2 : MonoBehaviour
{
    private void Awake()
    {

    }
    // Start is called before the first frame update
    public AudioSource audioSource;
    void Start()
    {
        audioSource.Play();
    }
}
