using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefabObject;
    public int objectNumberOnStart;

    private List<GameObject> poolObjects = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        //create the object needed
        for (int i = 0; i < objectNumberOnStart; i++)
        {
            CreateNewObject();
        }
    }


    // Update is called once per frame

    /// <summary>
    /// Instantiate new object
    /// </summary>
    private GameObject CreateNewObject()
    {
        GameObject gameObject = Instantiate(prefabObject);
        gameObject.SetActive(false);
        poolObjects.Add(gameObject);
        return gameObject;
    }
//Take from the list
public GameObject GetGameObject()
{
    //Find in the poolObject
    GameObject gameObject = poolObjects.Find(x => x.activeInHierarchy == false);
    if (gameObject == null)
    {
        gameObject = CreateNewObject();
    }
        gameObject.SetActive(true);
        return gameObject;
    
}
}
