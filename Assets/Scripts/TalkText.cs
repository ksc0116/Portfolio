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
            interactText.text = "이동하기 : G";
        }
        else
        {
            interactText.text = "대화하기 : G";
        }
    }
}
