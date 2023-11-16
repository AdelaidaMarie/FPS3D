using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cristal", menuName = "Cristal")]
public class CristalData : ScriptableObject
{

    [Header("Info")]
    public new string name;

    [Header("Shooting")]
    public int damage;
    public float maxDistance;
    public float ballSpeed;
    public float ballTime;
    private List<GameObject> poolObjects = new List<GameObject>();
    public int useMana;
    public float rate;
}