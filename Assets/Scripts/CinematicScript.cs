using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class CinematicScript : MonoBehaviour
{
    private Animator fade;
    private Image image;
    public Animator text1;
    public Animator text2;
    public Animator text3;

    void Start()
    {
        fade = GetComponent<Animator>();
        image = GetComponent<Image>();
        StartCoroutine(Blink());
        StartCoroutine(Credits());
    }
    IEnumerator Credits()
    {
        text1.Play("Fade1");
        yield return new WaitForSeconds(2f);
        text2.Play("Fade2");
        yield return new WaitForSeconds(2f);
        text3.Play("Fade3");
    }

    IEnumerator Blink()
    {
        yield return new WaitForSeconds(7f);
        fade.Play("FadeIn");
        yield return new WaitForSeconds(7f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
