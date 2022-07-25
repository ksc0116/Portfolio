using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inven_Manager : MonoBehaviour
{
    public Transform[] slots;
    public Transform[] q_slots;

    public bool isDrop;
    public bool isDragging;

    public Transform dragParent;
    public Transform curParent;
    public Transform curMousePosition;
    public Transform selectItem;


    public void GetItem(ItemInfo p_itemInfo)
    {
        for(int i = 0; i < q_slots.Length; i++)
        {
            if (q_slots[i].GetChild(1).gameObject.activeSelf == false)
            {
                continue;
            }
            else if(q_slots[i].GetChild(1).gameObject.activeSelf == true)
            {
                if (q_slots[i].GetChild(1).GetComponent<Image>().sprite == p_itemInfo.itemSprite)
                {
                    q_slots[i].GetChild(1).GetComponent<Item_Action>().m_itemInfo.Cnt++;
                    q_slots[i].GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = q_slots[i].GetChild(1).GetComponent<Item_Action>().m_itemInfo.Cnt.ToString();
                    return;
                }
            }
        }

        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i].GetChild(0).gameObject.activeSelf == false)
            {
                continue;
            }
            else if (slots[i].GetChild(0).gameObject.activeSelf == true)
            {
                if (slots[i].GetChild(0).GetComponent<Image>().sprite == p_itemInfo.itemSprite)
                {
                    slots[i].GetChild(0).GetComponent<Item_Action>().m_itemInfo.Cnt++;
                    slots[i].GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = slots[i].GetChild(0).GetComponent<Item_Action>().m_itemInfo.Cnt.ToString();
                    return;
                }
            }
        }

        for(int i=0;i<slots.Length;i++)
        {
            if (slots[i].GetChild(0).gameObject.activeSelf == false)
            {
                p_itemInfo.Cnt = 1;
                slots[i].GetChild(0).GetComponent<Image>().sprite = p_itemInfo.itemSprite;
                slots[i].GetChild(0).GetComponent<Item_Action>().m_itemInfo = p_itemInfo;
                slots[i].GetChild(0).gameObject.SetActive(true);
                Manager.instance.invenSlot_Manager.invenSlots[i].GetChild(0).GetComponent<Item_Action>().m_itemInfo = p_itemInfo;
                if (Manager.instance.quest_Manager.questIndex == 0)
                {
                    if (p_itemInfo.itemSprite.name=="sword")
                    {
                        Manager.instance.quest_Manager.swordCnt=1;
                    }
                }
                return;
            }
        }
    }
}
