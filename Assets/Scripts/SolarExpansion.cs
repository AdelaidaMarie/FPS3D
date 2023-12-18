using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SolarExpansion : MonoBehaviour

{
    public static SolarExpansion instance;
    public float duration;
    public int vibrato = 0;
    public float elasticity;
    private int counter = 3;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        counter += 3;
        transform.DOPunchScale(transform.localScale * counter, duration, vibrato, elasticity).OnComplete(Start);
    }

    // Update is called once per frame
    public float GetDuration()
    {
        return duration;
    }
}
