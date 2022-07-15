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

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;
            Color color = fadeImage.color;
            color.a = Mathf.Lerp(color.a, 0f, 0.01f); 
            fadeImage.color = color;
            yield return null;
        }
        gameObject.SetActive(false);
    }
    IEnumerator FadeOut()
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;
            Color color = fadeImage.color;
            color.a = Mathf.Lerp(color.a, 1f, 0.01f);
            fadeImage.color = color;
            yield return null;
        }
    }
}
