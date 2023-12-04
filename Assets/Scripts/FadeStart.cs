using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class FadeStart : MonoBehaviour
{
    public Animator fadeStart;
    // Start is called before the first frame update
    void Start()
    {
        fadeStart = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeSelf)
        fadeStart.Play("Fade4");
    }

}
