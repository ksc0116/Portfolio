using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponSlot : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI atkText;
    bool isFirst = false;
    private void Update()
    {
        if (Manager.instance.camera_Manager.isGameClear == true)
        {

        }

        atkText.text = Manager.instance.playerStat_Manager.atk.ToString();

        if (transform.GetComponentInChildren<Item_Action>() == null && Manager.instance.inven_Manager.isDragging == false)
        {
            isFirst = false;
            Manager.instance.playerStat_Manager.atk = Manager.instance.playerStat_Manager.originAtk;
            Manager.instance.playerStat_Manager.atk_Bonus = 0f;
        }

        if (isFirst == false)
        {
            if(transform.GetComponentInChildren<Item_Action>() != null)
            {
                isFirst = true;
                Manager.instance.playerStat_Manager.atk += transform.GetComponentInChildren<Item_Action>().m_itemInfo.atkBonus;
                
                transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}
