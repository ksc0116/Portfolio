using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
{
    Image fadeImage;
    float fadeTime=2.5f;

    private void Awake()
    {
        fadeImage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        StartCoroutine(Fade()); 
    }

    IEnumerator Fade()
    {
        yield return StartCoroutine("FadeOut");
        yield return StartCoroutine("FadeIn");
    }

    IEnumerator FadeIn()
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        Color color = fadeImage.color;

        while (color.a >0.01f)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;
            color = fadeImage.color;
            color.a = Mathf.Lerp(color.a, 0f, 0.03f); 
            fadeImage.color = color;
            yield return null;
        }
        gameObject.SetActive(false);
    }
    IEnumerator FadeOut()
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        Color color = fadeImage.color;

        while (color.a < 0.99f)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;
            color = fadeImage.color;
            color.a = Mathf.Lerp(color.a, 1f, 0.03f);
            fadeImage.color = color;
            yield return null;
        }
    }
}
