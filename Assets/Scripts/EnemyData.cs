using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Enemy/Data", order = 0)]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public string description;
    public float speed;
    public float shootFrequency;
    public Material enemyMaterial;
    public int maxLife;

    public float Speed { get => speed; }
    public float ShootFrequency { get => shootFrequency; set => shootFrequency = value; }
    public Material EnemyMaterial { get => enemyMaterial; }
    public int MaxLife { get => maxLife; }
}
