using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    
    [SerializeField] private int enemyNumber;
    [SerializeField] private GameObject enemyObject;
    private Coroutine spawnCoroutine;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(Spawn());
        }
    }
    IEnumerator Spawn()
    {
        InstantiateEnemy();
        yield return new WaitForSeconds(1.5f);
        if (GameObject.FindGameObjectsWithTag("Enemy").Length>= enemyNumber)
        {
            yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length < enemyNumber);
            spawnCoroutine = StartCoroutine(Spawn());
        }
        else 
        {
            spawnCoroutine = StartCoroutine(Spawn());
        }
    }
    private void InstantiateEnemy()
    {
        float positionX = Random.Range(transform.position.x - 15, transform.position.x + 30);
        float positionZ = Random.Range(transform.position.z - 15, transform.position.z + 30);
        
        Vector3 spawnPosition = new Vector3 (positionX, 1f, positionZ);
        Instantiate(enemyObject, spawnPosition, Quaternion.identity);
    }
    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        UnityEngine.AI.NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        bool foundPosition = false;
        while(!foundPosition)
        {
            if (UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
            {
                finalPosition = hit.position;
                foundPosition = true;
            }
        }
        return finalPosition;
    }
}
