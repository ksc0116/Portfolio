using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] GameObject invenObj;

    [SerializeField] TextMeshProUGUI goldText;

    private void Update()
    {
        if (invenObj.activeSelf == true)
        {
            goldText.text =Manager.instance.playerStat_Manager.playerGold.ToString()+"G";
        }
    }
}
