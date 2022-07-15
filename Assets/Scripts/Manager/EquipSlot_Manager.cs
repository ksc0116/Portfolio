using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSlot_Manager : MonoBehaviour
{
    public Transform[] equipSlots;
    private void Update()
    {

        //if (equipSlots[2].childCount == 1) return;

        //if (equipSlots[2].GetChild(1).gameObject.activeSelf == false) return;

        //Debug.Log($"{2} :{ equipSlots[2].GetChild(1).GetComponent<Item_Action>().m_itemInfo.itemSprite.name}");
    }
}
