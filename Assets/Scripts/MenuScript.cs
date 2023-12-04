using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject menu;
    public GameObject credits;
    public GameObject start;
    public AudioSource startSound;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Started()
    {
        startSound.Play();
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Starting()
    {
        menu.SetActive(false);
        start.SetActive(true);
        StartCoroutine(Started());
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Credits()
    {
        menu.SetActive(false);
        credits.SetActive(true);
    }
    public void Menu()
    {
        menu.SetActive(true);
        credits.SetActive(false);
    }
}
