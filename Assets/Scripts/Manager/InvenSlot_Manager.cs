using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenSlot_Manager : MonoBehaviour
{
    public Transform[] invenSlots;

    private void Update()
    {
        //for(int i = 0; i < invenSlots.Length; i++)
        //{
        //    if (invenSlots[i].childCount == 0) continue;
        //    if (invenSlots[i].GetChild(0).gameObject.activeSelf == false) continue;

        //    Debug.Log($"{i} :{ invenSlots[i].GetChild(0).GetComponent<Item_Action>().m_itemInfo.itemSprite.name}");
        //}
    }
}
