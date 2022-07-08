using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectImage : MonoBehaviour
{
    Image img;
    float fadeSpeed = 2f;
    private void Awake()
    {
        img = GetComponent<Image>();
    }

    private void OnEnable()
    {
        StartCoroutine("StartPingPong");
    }

    private void OnDisable()
    {
        StopCoroutine("StartPingPong");
    }

    IEnumerator StartPingPong()
    {
        while (true)
        {
            Color color=img.color;
            color.a = Mathf.Lerp(1, 0, Mathf.PingPong(Time.time * fadeSpeed, 1));
            img.color = color;
            yield return null;
        }
    }
}
