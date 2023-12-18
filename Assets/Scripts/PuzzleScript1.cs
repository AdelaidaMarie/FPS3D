using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleScript1 : MonoBehaviour
{
    public GameObject Button1;
    public GameObject Button2;
    bool Cinema;
    public GameObject Portal;
    public GameObject textMission1;
    public GameObject textMission2;
    public GameObject Final;
    // Start is called before the first frame update
    void Start()
    {
        Cinema = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Puzzle completed
        if (!Button1.activeSelf && !Button2.activeSelf && !Cinema)
        {
            Cinema = true;
            Portal.SetActive(false);
            textMission1.SetActive(false);
            textMission2.SetActive(true);
            Final.SetActive(true);
        }

    }



}
