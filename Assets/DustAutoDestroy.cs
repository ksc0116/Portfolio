using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustAutoDestroy : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine( AutoDestroy() );
    }
    IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
