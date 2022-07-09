using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HightLightEffect : MonoBehaviour
{
    [SerializeField] GameObject curQuestPanel;
    [SerializeField] Animator[] effectAnim;
    private void OnEnable()
    {
        curQuestPanel.SetActive(false);
    }
    public IEnumerator EffectOff()
    {
        for(int i = 0; i < effectAnim.Length; i++)
        {
            effectAnim[i].SetTrigger("onOff");
        }
        yield return new WaitForSeconds(2f);
        curQuestPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
