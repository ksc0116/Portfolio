using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatFrame : MonoBehaviour
{
    [SerializeField] GameObject frame;


    [SerializeField] TextMeshProUGUI levText;
    [SerializeField] TextMeshProUGUI expText;
    [SerializeField] TextMeshProUGUI atkText;
    [SerializeField] TextMeshProUGUI defText;
    private void Update()
    {
        if (frame.activeSelf == true)
        {
            levText.text = Manager.instance.playerStat_Manager.lev.ToString();
            expText.text = Manager.instance.playerStat_Manager.curExp.ToString()+" %";
            atkText.text = Manager.instance.playerStat_Manager.atk.ToString();
            defText.text = (Manager.instance.playerStat_Manager.def + Manager.instance.playerStat_Manager.def_Bonus).ToString();
        }
    }
}
