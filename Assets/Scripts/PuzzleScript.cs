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
    public GameObject textMission1;
    public GameObject textMission2;
    public GameObject Portal2;
    // Start is called before the first frame update
    void Start()
    {
        Cinema = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Puzzle completed
        if (!Button1.activeSelf && !Button2.activeSelf && !Button3.activeSelf && !Button4.activeSelf && !Cinema) 
        {
            Cinema = true;
            Portal.SetActive(true);
            textMission1.SetActive(false);
            textMission2.SetActive(true);
            Portal2.SetActive(false);
        }

    }
    

    
}
