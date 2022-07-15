using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TalkText : MonoBehaviour
{
    TextMeshProUGUI interactText;
    private void Awake()
    {
        interactText = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        if (Manager.instance.playerStat_Manager.isPortal == true)
        {
            interactText.text = "�̵��ϱ� : G";
        }
        else
        {
            interactText.text = "��ȭ�ϱ� : G";
        }
    }
}
