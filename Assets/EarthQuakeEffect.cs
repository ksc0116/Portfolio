using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthQuakeEffect : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(AutoDestroy());
    }
    IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }
}
