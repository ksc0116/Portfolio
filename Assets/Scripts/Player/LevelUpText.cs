using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpText : MonoBehaviour
{
    [SerializeField] GameObject levelUpText;
    [SerializeField] GameObject levelUpParticle;

    private void OnEnable()
    {
        StartCoroutine(AutoDisable());
    }

    IEnumerator AutoDisable()
    {
        yield return new WaitForSeconds(1f);
        levelUpText.SetActive(false);
        levelUpParticle.SetActive(false);
    }
}
