using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleScript : MonoBehaviour
{
    public GameObject Button1;
    public GameObject Button2;
    public GameObject Button3;
    public GameObject Button4;
    bool Cinema;
    public GameObject Portal;
    // Start is called before the first frame update
    void Start()
    {
        Cinema = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!Button1.activeSelf && !Button2.activeSelf && !Button3.activeSelf && !Button4.activeSelf && !Cinema) 
        {
            Cinema = true;
            Portal.SetActive(true);
        }

    }
    

    
}
