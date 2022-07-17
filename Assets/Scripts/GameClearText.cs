using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameClearText : MonoBehaviour
{
    TextMeshProUGUI clearText;
    private void Awake()
    {
        clearText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        StartCoroutine(OnAble());
    }
    IEnumerator OnAble()
    {
        while (clearText.rectTransform.localScale != Vector3.one)
        {
            clearText.rectTransform.localScale = Vector3.Lerp(clearText.rectTransform.localScale, Vector3.one, 0.01f);
            yield return null;
        }
    }
}
